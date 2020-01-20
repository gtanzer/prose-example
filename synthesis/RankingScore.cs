using System;
using Microsoft.ProgramSynthesis;
using Microsoft.ProgramSynthesis.AST;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Features;

namespace Ex
{
    public class RankingScore : Feature<double>
    {
        public RankingScore(Grammar grammar) : base(grammar, "Score") { }
        
        [FeatureCalculator(nameof(Semantics.Substring))]
        public static double Substring(double v, double start, double end) => v + start + end - 1;

        [FeatureCalculator(nameof(Semantics.Append))]
        public static double Append(double v, double w) => v + w - 1;

        [FeatureCalculator("a2", Method = CalculationMethod.FromLiteral)]
        public static double A2(int a2) => -1.0;
    }
}
