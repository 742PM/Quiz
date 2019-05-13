using Newtonsoft.Json;

namespace QuizWebHookBot.DTO
{
    public class TaskInfoDTO
    {
        public TaskInfoDTO(string question, string[] answers)
        {
            Question = question;
            Answers = answers;
        }

        [JsonProperty("question")] public string Question { get; }
        [JsonProperty("answers")] public string[] Answers { get; }
    }
}
