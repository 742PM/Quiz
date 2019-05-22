using Newtonsoft.Json;

namespace QuizRequestExtendedService.DTO
{
    public class TemplateGeneratorDTO
    {
        [JsonProperty("question")] public string Question { get; }
        [JsonProperty("text")] public string Text { get; }
        [JsonProperty("possibleAnswers")] public string[] PossibleAnswers { get; }
        [JsonProperty("answer")] public string Answer { get; }
        [JsonProperty("hints")] public string[] Hints { get; }
        [JsonProperty("streak")] public int Streak { get; }

        public TemplateGeneratorDTO(
            string question,
            string text,
            string[] possibleAnswers,
            string answer,
            string[] hints,
            int streak)
        {
            Text = text;
            PossibleAnswers = possibleAnswers;
            Answer = answer;
            Hints = hints;
            Streak = streak;
            Question = question;
        }
    }
}