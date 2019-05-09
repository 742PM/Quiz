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
            var so = CreateScriptObject(randomSeed);

            var questionAndAnswerStorage = Concat(TemplateCode, Answer);
            var hintsStorage = Concat(Hints ?? new string[0]);
            var answersStorage = Concat(PossibleAnswers ?? new string[0]);

            //var (question, answer) = result[0];
            var fields = new[] { questionAndAnswerStorage, hintsStorage, answersStorage };

            var ((question, answer), hints, answers)
                = fields.MapMany(vs => Concat(vs).Map(s => Template.Parse(s).Render(so)).Split())
                        .Select(r => r.Split().ToArray())
                        .ToArray();
            return new Task(question, hints, answer, Id, answers);
        }

        private static ScriptObject CreateScriptObject(Random random) => TemplateLanguage.Create(random);
    }
}