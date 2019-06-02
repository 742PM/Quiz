using Newtonsoft.Json;

namespace QuizWebApp.Services.TaskService.DTO
{
    public class TemplateGeneratorForRenderDTO
    {
        [JsonProperty("text")] public string Text { get; }
        [JsonProperty("question")] public string Question { get; }
        [JsonProperty("possibleAnswers")] public string[] PossibleAnswers { get; }
        [JsonProperty("answer")] public string Answer { get; }
        [JsonProperty("hints")] public string[] Hints { get; }

        public TemplateGeneratorForRenderDTO(
            string text,
            string[] possibleAnswers,
            string answer,
            string[] hints,
            string question)
        {
            Text = text;
            PossibleAnswers = possibleAnswers;
            Answer = answer;
            Hints = hints;
            Question = question;
        }
    }
}