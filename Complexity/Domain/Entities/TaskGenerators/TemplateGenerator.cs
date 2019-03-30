using System;
using Domain.Values;
using Infrastructure;

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
        public override Task GetTask(Random randomSeed) => new Task(Randomize(randomSeed), Hints, Answer, Id, null);

        private string Randomize(Random randomSeed) =>
            TemplateCode.Replace("$i$", ((char) randomSeed.Next('a', 'z')).ToString());
    }
}
