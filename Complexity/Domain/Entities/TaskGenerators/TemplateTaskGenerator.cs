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
        }

        [MustBeSaved] public string[] PossibleAnswers { get; }

        [MustBeSaved] public string TemplateCode { get; }

        [MustBeSaved] public string[] Hints { get; }

        /// <summary>
        ///     Should not be used as real answer for user;
        /// </summary>
        [MustBeSaved]
        public string Answer { get; }

        /// <inheritdoc />
        public override Task GetTask(Random randomSeed)
        {
            var (question, answer, answers, hints) = RenderFields(CreateScriptObject(randomSeed));

            return new Task(question, hints, answer, Id, answers);
        }

        private (string question, string answer, string[] answers, string[] hints) RenderFields(IScriptObject so)
        {
            var doHintsExist = Hints?.Length > 0;
            var doAnswersExist = PossibleAnswers?.Length > 0;
            var (head, rest) = Template.Parse(
                new[]
                    {
                        new[] {TemplateCode, Answer}.SafeConcat(out var qaKey), Hints?.SafeConcat(out var hintsKey),
                        PossibleAnswers?.SafeConcat(out var answersKey)
                    }.Where(s => s != "")
                    .ToList().SafeConcat(out var key)).Render(so).SafeSplit(key);
            var questionAndAnswer = head.SafeSplit(qaKey);
            string[] answers;
            string[] hints;
            if (doHintsExist && doAnswersExist)
            {
                answers = rest[1].SafeSplit(answersKey).ToArray();
                hints = rest[0].SafeSplit(hintsKey).ToArray();
            }
            else if (!doHintsExist)
            {
                answers = rest[0].SafeSplit(answersKey).ToArray();
                hints = new string[0];
            }
            else
            {
                hints = rest[0].SafeSplit(hintsKey).ToArray();
                answers = new string[0];
            }

            return (questionAndAnswer[0], questionAndAnswer[1], answers, hints);
        }

        private static ScriptObject CreateScriptObject(Random random) => TemplateLanguage.Create(random);
    }
}