namespace Application.Info
{
    public class TaskInfo
    {
        public TaskInfo(string question, string[] answers)
        {
            Question = question;
            Answers = answers;
        }

        public string Question { get; }
        public string[] Answers { get; }
    }
}
