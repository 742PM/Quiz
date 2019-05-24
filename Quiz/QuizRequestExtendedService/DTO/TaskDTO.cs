using System.Collections.Generic;
using Newtonsoft.Json;

namespace QuizRequestExtendedService.DTO
{
    public class TaskDTO
    {
        [JsonProperty("question")] public string Question { get; set; }
        [JsonProperty("text")] public string Text { get; set; }
        public IEnumerable<string> Answers { get; set; }
        public bool HasHints { get; set; }

        public TaskDTO(string question, string text, IEnumerable<string> answers, bool hasHints)
        {
            Question = question;
            Text = text;
            Answers = answers;
            HasHints = hasHints;
        }
    }
}