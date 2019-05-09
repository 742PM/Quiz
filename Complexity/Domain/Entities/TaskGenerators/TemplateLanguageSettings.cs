using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Scriban.Runtime;

namespace Domain.Entities.TaskGenerators
{
    public partial class TemplateLanguage
    {
        


        private static readonly Dictionary<string, Func<Random, string>> NumericalSubstitutions =
            new Dictionary<string, Func<Random, string>>
            {
                [Const] = r =>
                    AnyOf(r,
                        new ScriptArray(Enumerable.Range(-MaxRandomConstantValue, MaxRandomConstantValue)
                            .Where(x => x != 0))).ToString(),
                [From] = r => r.Next(-MaxRandomConstantValue, MaxRandomConstantValue).ToString(),
                [IterateConstant] = r => r.Next(LoopAmount >> 2, LoopAmount).ToString()
            };

        private static readonly Dictionary<string, string[]> PossibleLiteralSubstitutions =
            new Dictionary<string, string[]>
            {
                [LoopVariable] = LoopVariables.ToArray(),
                [To] = Tos.ToArray(),
                [SimpleOperation] = Operations.ToArray()
            };

        private TemplateLanguage()
        {
        }


        public static ScriptObject Create(Random random)
        {
            var language = new TemplateLanguage();
            var so = new ScriptObject();
            AddMethods(random, so);
            so.Import(language);

            var substitutionValues = GenerateLiteralSubstitutions(random)
                                    .Concat(NumericalSubstitutions.Select(kv => (kv.Key, kv.Value(random))));

            foreach (var (substitution, value) in substitutionValues)
                so.Add(substitution, value);

            return so;
        }

        private static void AddMethod<TOut, TIn>(Func<Random, TIn, TOut> method, Random random, ScriptObject so)
        {
            so.Import(method.Method.Name.ToSnakeCase(), new Func<TIn, TOut>(item => method(random, item)));
        }

        private static void AddMethod<TOut, TIn1, TIn2>(Func<Random, TIn1, TIn2, TOut> method, Random random,
            ScriptObject so)
        {
            so.Import(method.Method.Name, new Func<TIn1, TIn2, TOut>((first, second) => method(random, first, second)));
        }

        public static IEnumerable<(string, string)> GenerateLiteralSubstitutions(Random random)
        {
            var takenValues = new HashSet<string>();
            foreach (var (substitution, values) in PossibleLiteralSubstitutions)
            {
                if (values.Length < BaseTemplateKeywordsAmount)
                    throw new ArgumentException($"There are not enough values to substitute every {substitution}");
                for (var i = 0; i < BaseTemplateKeywordsAmount; i++)
                {
                    var value = values.Where(v => !takenValues.Contains(v)).TakeRandomOne(random);
                    takenValues.Add(value);
                    yield return ($"{substitution}{i}", value);
                }
            }
        }
    }
}