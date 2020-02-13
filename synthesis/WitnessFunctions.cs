using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Learning;

namespace Ex {
    public class WitnessFunctions : DomainLearningLogic {
        public WitnessFunctions(Grammar grammar) : base(grammar) { }
        
        [WitnessFunction(nameof(Semantics.Append), 1)]
        public DisjunctiveExamplesSpec WitnessSuffix(GrammarRule rule, ExampleSpec spec) {
            var result = new Dictionary<State, IEnumerable<object>>();
            foreach (var example in spec.Examples) {
                State inputState = example.Key;
                var output = example.Value as string;
                var substrings = new List<string>();
                for (int i = 1; i <= output.Length; ++i) {
                    substrings.Add(output.Substring(i));
                }
                if (substrings.Count == 0) return null;
                result[inputState] = substrings.Cast<object>();
                Console.WriteLine("Suffix o: {0}\tp: {1}", output, String.Join(", ", substrings));
            }
            return new DisjunctiveExamplesSpec(result);
        }
        
        [WitnessFunction(nameof(Semantics.Append), 0, DependsOnParameters = new []{1})]
        public ExampleSpec WitnessPrefix(GrammarRule rule, ExampleSpec spec, ExampleSpec suffixSpec) {
            var result = new Dictionary<State, object>();
            foreach (var example in spec.Examples) {
                State inputState = example.Key;
                var output = example.Value as string;
                var suffix = (string) suffixSpec.Examples[inputState];
                var prefix = output.Substring(0, (output.Length-suffix.Length));
                result[inputState] = prefix;
                Console.WriteLine("Prefix o: {0}\tp: {1}\ts: {2}", output, prefix, suffix);
            }
            return new ExampleSpec(result);
        }
    }
}
