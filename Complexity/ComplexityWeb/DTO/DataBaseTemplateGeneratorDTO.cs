using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class DataBaseTemplateGeneratorDTO
    {
        [JsonProperty("template")] public string Template { get; }
        [JsonProperty("possible_answers")] public IEnumerable<string> PossibleAnswers { get; }
        [JsonProperty("right_answer")] public string RightAnswer { get; }
        [JsonProperty("hints")] public IEnumerable<string> Hints { get; }

        public DataBaseTemplateGeneratorDTO(string template, IEnumerable<string> possibleAnswers, string rightAnswer, IEnumerable<string> hints)
        {
            Template = template;
            PossibleAnswers = possibleAnswers;
            RightAnswer = rightAnswer;
            Hints = hints;
        }
    }
}