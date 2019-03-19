namespace Application
{
    public class TaskDescription
    {
        public TaskDescription(string question, string[] answers)
        {
            Question = question;
            Answers = answers;
        }

        public string Question { get; }

        public string[] Answers { get; }
    }
}