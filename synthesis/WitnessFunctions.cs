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

        /*
        // old version
        [WitnessFunction(nameof(Semantics.Append), 0)]
        public DisjunctiveExamplesSpec WitnessPrefix(GrammarRule rule, ExampleSpec spec) {
            var result = new Dictionary<State, IEnumerable<object>>();
            foreach (var example in spec.Examples) {
                State inputState = example.Key;
                var output = example.Value as string;
                var substrings = new List<string>();
                for (int i = 1; i <= output.Length; ++i) {
                    substrings.Add(output.Substring(0, i));
                }
                if (substrings.Count == 0) return null;
                result[inputState] = substrings.Cast<object>();
                Console.WriteLine("Prefix o: {0}\tp: {1}", output, String.Join(", ", substrings));
            }
            return new DisjunctiveExamplesSpec(result);
        }*/
        
        // new disjunctified version
        [WitnessFunction(nameof(Semantics.Append), 0)]
        public DisjunctiveExamplesSpec WitnessPrefix(GrammarRule rule, DisjunctiveExamplesSpec spec) {
            var result = new Dictionary<State, IEnumerable<object>>();
            
            Console.WriteLine("Starting Prefix");
            
            foreach (var example in spec.DisjunctiveExamples) {
                State inputState = example.Key;
                var prefixes = new HashSet<string>();
                Console.WriteLine("inputState: {0}", inputState);
                foreach(string output in example.Value) {
                    Console.WriteLine("Output: {0}", output);
                    for (int i = 1; i <= output.Length; ++i) {
                        prefixes.Add(output.Substring(0, i));
                    }
                    Console.WriteLine("Prefix o: {0}\tp: {1}", output, String.Join(", ", prefixes));
                }
                if (prefixes.Count == 0) return null;
                result[inputState] = prefixes.ToList().Cast<object>();
            }
            return new DisjunctiveExamplesSpec(result);
        }

        // old version
        /*[WitnessFunction(nameof(Semantics.Append), 1, DependsOnParameters = new []{0})]
        public ExampleSpec WitnessSuffix(GrammarRule rule, ExampleSpec spec, ExampleSpec prefixSpec) {
            var result = new Dictionary<State, object>();
            foreach (var example in spec.Examples) {
                State inputState = example.Key;
                var output = example.Value as string;
                var prefix = (string) prefixSpec.Examples[inputState];
                result[inputState] = output.Substring(prefix.Length);
                Console.WriteLine("Suffix o: {0}\tp: {1}\ts: {2}", output, prefix, output.Substring(prefix.Length));
            }
            return new ExampleSpec(result);
        }*/
        
        // new disjunctified version
        [WitnessFunction(nameof(Semantics.Append), 1, DependsOnParameters = new []{0})]
        public DisjunctiveExamplesSpec WitnessSuffix(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec prefixSpec) {
            var result = new Dictionary<State, IEnumerable<object>>();
            Console.WriteLine("Suffix dispatched");
            return null;
            /*
            foreach (var example in spec.DisjunctiveExamples) {
                State inputState = example.Key;
                var suffixes = new HashSet<string>();
                var prefix = (string) prefixSpec.Examples[inputState];
                foreach (string output in example.Value) {
                    suffixes.Add(output.Substring(prefix.Length));
                    Console.WriteLine("Suffix o: {0}\tp: {1}\ts: {2}", output, prefix, output.Substring(prefix.Length));
                }
                if (suffixes.Count == 0) return null;
                result[inputState] = suffixes.ToList().Cast<object>();
            }*/
            
            return new DisjunctiveExamplesSpec(result);
        }
        
    }
}
