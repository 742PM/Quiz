using System;
using System.Linq;
using Domain.Values;
using Infrastructure;
using Scriban;
using Scriban.Runtime;
using static Infrastructure.Storage;

namespace Domain.Entities.TaskGenerators
{
    public class TemplateTaskGenerator : TaskGenerator
    {
        public TemplateTaskGenerator(
            Guid id,
            string[] possibleAnswers,
            string templateCode,
            string[] hints,
            string answer,
            int streak,
            string question = "You have the code, guess a question") : base(id, streak)
        {
            PossibleAnswers = possibleAnswers ?? throw new ArgumentException($"{nameof(possibleAnswers)} are null");
            TemplateCode = templateCode;
            Hints = hints ?? throw new ArgumentException($"{nameof(hints)} are null");
            Answer = answer;
            Question = question;
        }

        [MustBeSaved] public string[] PossibleAnswers { get; }

        [MustBeSaved] public string TemplateCode { get; }

        [MustBeSaved] public string Question { get; }

        [MustBeSaved] public string[] Hints { get; }

        /// <summary>
        ///     Should not be used as real answer for user;
        /// </summary>
        [MustBeSaved]
        public string Answer { get; }

        /// <inheritdoc />
        public override Task GetTask(Random randomSeed)
        {
            var so = CreateScriptObject(randomSeed);

            var simpleFieldsStorage = Concat(TemplateCode, Answer, Question);
            var hintsStorage = Concat(Hints ?? new string[0]);
            var answersStorage = Concat(PossibleAnswers ?? new string[0]);

            var fields = new[] { simpleFieldsStorage, hintsStorage, answersStorage };

            var ((code, answer, question), hints, answers)
                = fields.MapMany(vs => Concat(vs).Map(s => Template.Parse(s).Render(so)).Split())
                        .Select(r => r.Split().ToArray())
                        .ToArray();
            return new Task(code, hints, answer, Id, answers, question);
        }

        private static ScriptObject CreateScriptObject(Random random) => TemplateLanguage.Create(random);
    }
}