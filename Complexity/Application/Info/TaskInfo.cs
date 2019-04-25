namespace Application.Info
{
    public class TaskInfo
    {
        public TaskInfo(string question, string[] answers)
        {
            Question = question;
            Answers = answers;
        }

        /// <inheritdoc />
        public override string ToString() => $"{nameof(Question)}: {Question}, {nameof(Answers)}: {Answers}";

        public string Question { get; }
        public string[] Answers { get; }
    }

}
