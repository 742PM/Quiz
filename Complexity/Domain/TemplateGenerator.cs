using System;

namespace Domain
{
    public class TemplateTaskGenerator : TaskGenerator
    {

        public TemplateTaskGenerator(
            Guid id,
            string[] possibleAnswers,
            string templateCode,
            string[] hints,
            string answer)
        {
            Id = id;
            PossibleAnswers = possibleAnswers;
            TemplateCode = templateCode;
            Hints = hints;
            Answer = answer;
        }

        [MustBeSaved]
        public string[] PossibleAnswers { get; }

        [MustBeSaved]
        public string TemplateCode { get; }

        [MustBeSaved]
        public string[] Hints { get; }

        [MustBeSaved]
        public string Answer { get; }

        [MustBeSaved]
        public override Guid Id { get; }

        /// <inheritdoc />
        public override Task GetTask(Random randomSeed) => new Task(Randomize(randomSeed), Hints, Answer, Id);

        private string Randomize(Random randomSeed) =>
            TemplateCode.Replace("$i$", ((char) randomSeed.Next('a', 'z')).ToString());
    }
}
