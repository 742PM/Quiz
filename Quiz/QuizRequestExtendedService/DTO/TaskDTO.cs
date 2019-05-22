using System.Collections.Generic;

namespace QuizRequestExtendedService.DTO
{
    public class TaskDTO
    {
        public string Question { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> Answers { get; set; }
        public bool HasHints { get; set; }

        public TaskDTO(string question, IEnumerable<string> answers, bool hasHints, string text)
        {
            Question = question;
            Text = text;
            Answers = answers;
            HasHints = hasHints;
        }
    }
}