using System;
using System.Collections.Generic;
using System.Linq;
using Scriban;
using Scriban.Runtime;

namespace Domain.Entities.TaskGenerators
{
    public partial class TemplateLanguage
    {
        private TemplateLanguage(Random random)
        {
            Random = random;
        }

        public static ScriptObject Create(Random random)
        {
            var language = new TemplateLanguage(random);
            var so = new ScriptObject();
            AddMethod<object, ScriptArray>(AnyOf, random, so);
            so.Import(language);
            return so;
        }

        private static void AddMethod<TOut, TIn>(Func<Random, TIn, TOut> method, Random random, ScriptObject so)
        {
            so.Import(method.Method.Name, new Func<TIn, TOut>(item => method(random, item)));
        }
    }
    public static class TaskRandomizer
    {
        public const string LoopVariable = "loop_var";
        public const string Const = "const";
        public const string From = "from";
        public const string To = "to";
        public const string IterateConstant = "iter";
        public const int LoopAmount = 8;
        public const int MaxRandomConstantValue = 50;
        public const int BaseTemplateKeywordsAmount = 5;
        public static readonly IReadOnlyCollection<string> LoopVariables = new[] { "i", "j", "k", "item", "x", "y", "element", "step" };
        public static readonly IReadOnlyCollection<string> Tos = new[] { "n", "m", "length", "amount", "size" };


        private static readonly Dictionary<string, Func<Random, string>> NumericalSubstitutions =
            new Dictionary<string, Func<Random, string>>
            {
                [Const] = r => r.Next(-MaxRandomConstantValue, MaxRandomConstantValue).ToString(),
                [From] = r => r.Next(-MaxRandomConstantValue, MaxRandomConstantValue).ToString(),
                [IterateConstant] = r => r.Next(LoopAmount >> 2, LoopAmount).ToString()
            };

        private static readonly Dictionary<string, string[]> PossibleLiteralSubstitutions =
            new Dictionary<string, string[]>
            {
                [LoopVariable] = LoopVariables.ToArray(),
                [To] = Tos.ToArray()
            };



        public static IEnumerable<(string, string)> GenerateLiteralSubstitutions(Random random)
        {
            var takenValues = new HashSet<string>();
            foreach (var (substitution, values) in PossibleLiteralSubstitutions)
            {
                if (values.Length < BaseTemplateKeywordsAmount)
                    throw new ArgumentException($"There are not enough values to substitute every {substitution}");
                for (var i = 0; i < BaseTemplateKeywordsAmount; i++)
                {
                    var value = GetRandomLiteralValue(values.Where(v => !takenValues.Contains(v)), random);
                    takenValues.Add(value);
                    yield return ($"{substitution}{i}", value);
                }
            }
        }

        public static string GetRandomLiteralValue(IEnumerable<string> possibleValues, Random random)
        {
            return possibleValues.TakeRandom(random: random).First();
        }


        public static string Randomize(this Template template, Random randomSeed) => template.Render(GetRandomizedProperties(randomSeed));


        private static ScriptObject GetRandomizedProperties(Random random)
        {
            var result = new ScriptObject();
            foreach (var (substitution, value) in GenerateLiteralSubstitutions(random)
                .Concat(NumericalSubstitutions.Select(kv => (kv.Key, kv.Value(random)))))
            {
                result.Add(substitution, value);
            }

            return result;
        }
    }
}