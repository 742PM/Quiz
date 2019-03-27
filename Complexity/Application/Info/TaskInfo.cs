namespace Application.Info
{
    public class TaskInfo
    {
        public string Question { get; }
        public string[] Answers { get; }

        public TaskInfo(string question, string[] answers)
        {
            Question = question;
            Answers = answers;
        }
    }
}