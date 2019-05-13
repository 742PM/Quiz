using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class DataBaseTemplateGeneratorWithStreakDTO
    {
        [JsonProperty("template")] public string Template { get; }
        [JsonProperty("question")] public string Question { get; }
        [JsonProperty("possible_answers")] public IEnumerable<string> PossibleAnswers  { get; }
        [JsonProperty("right_answer")] public string RightAnswer { get; }
        [JsonProperty("hints")] public IEnumerable<string> Hints { get; }
        [JsonProperty("streak")] public int Streak { get; }

        public DataBaseTemplateGeneratorWithStreakDTO(string template, IEnumerable<string> possibleAnswers, string rightAnswer, IEnumerable<string> hints, int streak, string question)
        {
            Template = template;
            PossibleAnswers = possibleAnswers;
            RightAnswer = rightAnswer;
            Hints = hints;
            Streak = streak;
            Question = question;
        }
    }
}