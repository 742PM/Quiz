namespace Application.Info
{
    public class TaskInfo
    {
        public TaskInfo(string question, string[] answers, bool hasHints, string text)
        {
            Question = question;
            Answers = answers;
            HasHints = hasHints;
            Text = text;
        }

        /// <inheritdoc />
        public override string ToString() =>
            $"{nameof(Question)}: {Question}, {nameof(Answers)}: {Answers}, {nameof(HasHints)}: {HasHints}";

        public string Question { get; }
        public string[] Answers { get; }
        public bool HasHints { get; }
        public string Text { get; }
    }
}