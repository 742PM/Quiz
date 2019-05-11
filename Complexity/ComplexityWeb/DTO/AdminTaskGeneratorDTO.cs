using System;
using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class AdminTaskGeneratorDTO
    {
        public AdminTaskGeneratorDTO(Guid id, string text, int streak, string question, string[] possibleAnswers, string[] hints, string answer)
        {
            Id = id;
            Text = text;
            Streak = streak;
            Question = question;
            PossibleAnswers = possibleAnswers;
            Hints = hints;
            Answer = answer;
        }

        [JsonProperty("id")] public Guid Id { get; }
        
        [JsonProperty("text")] public string Text { get; }

        [JsonProperty("streak")] public int Streak { get; }

        [JsonProperty("question")] public string Question { get; }

        [JsonProperty("possibleAnswers")] public string[] PossibleAnswers { get; }

        [JsonProperty("hints")] public string[] Hints { get; }

        [JsonProperty("answer")] public string Answer { get; }
    }
}