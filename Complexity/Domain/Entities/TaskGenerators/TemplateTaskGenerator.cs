using System;
using System.Linq;
using Domain.Values;
using Infrastructure;
using Scriban;
using Scriban.Runtime;

namespace Domain.Entities.TaskGenerators
{
    public class TemplateTaskGenerator : TaskGenerator
    {
        public const string LoopVariable = "loop_var";
        public const string Const = "const";
        public const string From = "from";
        public const string To = "to";
        public const string IterateConstant = "iter";
        public const int LoopAmount = 8;

        public const int MaxRandomConstantValue = 50;
        private const int BaseTemplateKeywordsAmount = 5;
        private readonly char[] letters = Enumerable.Range('a', 26).Select(i => (char) i).ToArray();

        private readonly Template template;

        public TemplateTaskGenerator(
            Guid id,
            string[] possibleAnswers,
            string templateCode,
            string[] hints,
            string answer,
            int streak) : base(id, streak)
        {
            PossibleAnswers = possibleAnswers;
            TemplateCode = templateCode;
            Hints = hints;
            Answer = answer;
            template = Template.Parse(TemplateCode);
        }

        [MustBeSaved] public string[] PossibleAnswers { get; }

        [MustBeSaved] public string TemplateCode { get; }

        [MustBeSaved] public string[] Hints { get; }

        /// <summary>
        ///     Should not be used as real answer for user;
        /// </summary>
        [MustBeSaved]
        public string Answer { get; }

        private ScriptObject GetRandomizedProperties(Random random)
        {
            var result = new ScriptObject
            {
                {LoopVariable, letters[random.Next(0, 25)]},
                {Const, random.Next(-MaxRandomConstantValue, MaxRandomConstantValue)}
            };
            foreach (var i in Enumerable.Range(1, BaseTemplateKeywordsAmount))
            {
                result.Add($"{LoopVariable}{i}", letters[random.Next(0, 25)]);
                result.Add($"{Const}{i}", random.Next(-MaxRandomConstantValue / 2, MaxRandomConstantValue / 2));
                var fromValue = random.Next(-MaxRandomConstantValue, MaxRandomConstantValue);
                var toValue = fromValue + random.Next(0, LoopAmount * MaxRandomConstantValue);
                result.Add($"{From}{i}", fromValue);
                result.Add($"{To}{i}", "n");
                result.Add($"{IterateConstant}{i}", random.Next(2, 8));
            }

            return result;
        }

        /// <inheritdoc />
        public override Task GetTask(Random randomSeed) =>
            new Task(Randomize(randomSeed), Hints, Answer, Id, PossibleAnswers);

        private string Randomize(Random randomSeed) => template.Render(GetRandomizedProperties(randomSeed));
    }
}
