using System;
using System.Collections.Generic;
using Infrastructure.Extensions;
using Scriban.Runtime;
using static System.Linq.Enumerable;

namespace Domain.Entities.TaskGenerators
{
    public partial class TemplateLanguage
    {
        [ScriptMemberIgnore] public const string LoopVariable = "loop_var";

        [ScriptMemberIgnore] public const string Const = "const";

        [ScriptMemberIgnore] public const string From = "from";

        [ScriptMemberIgnore] public const string To = "to";

        [ScriptMemberIgnore] public const string SimpleOperation = "simple_operation";

        [ScriptMemberIgnore] public const string IterateConstant = "iter";

        [ScriptMemberIgnore] public const int LoopAmount = 8;

        public const int MaxRandom = 50;

        [ScriptMemberIgnore] public const int BaseTemplateKeywordsAmount = 5;

        private static readonly Dictionary<string, Func<Random, string>> NumericalSubstitutions =
            new Dictionary<string, Func<Random, string>>
            {
                [Const] = random => AnyOf(random,
                    new ScriptArray(Range(-MaxRandom, MaxRandom).Where(x => x != 0))).ToString(),
                [From] = random => random.Next(2, MaxRandom).ToString(),
                [IterateConstant] = random => random.Next(LoopAmount >> 2, LoopAmount).ToString()
            };

        private static readonly Dictionary<string, string[]> PossibleLiteralSubstitutions =
            new Dictionary<string, string[]>
            {
                [LoopVariable] = new[]
                    {"i", "j", "k", "x", "y", "step"},
                [To] = new[] {"n", "m", "length", "amount", "size"},
                [SimpleOperation] = new[]
                    {
                        "c++", "k1--", "service.Update()", "queue.Pop()",
                        "k3++"
                    }.Select(s => s + ";")
                    .ToArray()
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
                .Concat(NumericalSubstitutions.SelectMany(kv => Range(1, 5)
                    .Select(i => ($"{kv.Key}{i}", kv.Value(random)))));

            foreach (var (substitution, value) in substitutionValues)
                so.Add(substitution, value);

            return so;
        }

        public static List<SubstitutionData> GetValuesExample()
        {
            return Create(new Random()).Select(kv => new SubstitutionData(kv)).ToList();
        }

        private static void AddMethod<TOut, TIn>(Func<Random, TIn, TOut> method, Random random, ScriptObject so)
        {
            so.Import(method.Method.Name.ToSnakeCase(), new Func<TIn, TOut>(item => method(random, item)));
        }

        private static void AddMethod<TOut, TIn1, TIn2>(
            Func<Random, TIn1, TIn2, TOut> method,
            Random random,
            ScriptObject so)
        {
            so.Import(method.Method.Name.ToSnakeCase(),
                new Func<TIn1, TIn2, TOut>((first, second) => method(random, first, second)));
        }
        private static void AddInstanceMethod<TOut, TIn1>(
            Func<Random, ScriptObject, TIn1, TOut> method,
            Random random,
            ScriptObject so)
        {
            so.Import(method.Method.Name.ToSnakeCase(),
                new Func<TIn1, TOut>(first => method(random,so ,first )));
        }
        private static void AddInstanceMethod<TOut, TIn1,TIn2>(
            Func<Random, ScriptObject, TIn1,TIn2, TOut> method,
            Random random,
            ScriptObject so)
        {
            so.Import(method.Method.Name.ToSnakeCase(),
                new Func<TIn1,TIn2, TOut>((first,second) => method(random, so, first,second)));
        }

        private static IEnumerable<(string, string)> GenerateLiteralSubstitutions(Random random)
        {
            var takenValues = new HashSet<string>();
            foreach (var (substitution, values) in PossibleLiteralSubstitutions)
            {
                if (values.Length < BaseTemplateKeywordsAmount)
                    throw new ArgumentException($"There are not enough values to substitute every {substitution}");
                for (var i = 1; i <= BaseTemplateKeywordsAmount; i++)
                {
                    var value = values.Where(v => !takenValues.Contains(v)).TakeRandomOne(random);
                    takenValues.Add(value);
                    yield return ($"{substitution}{i}", value);
                }
            }
        }

        public struct SubstitutionData
        {
            public string Substitution { get; }
            public string Value { get; }

            public SubstitutionData(string substitution, object value)
            {
                Substitution = substitution;
                Value = value.ToString().Contains("GenericFunctionWrapper")
                    ? $"Function {substitution}"
                    : value.ToString();
            }

            public SubstitutionData(KeyValuePair<string, object> pair) : this(pair.Key, pair.Value)
            {
            }
        }
    }
}