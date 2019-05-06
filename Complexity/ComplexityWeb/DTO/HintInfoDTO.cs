using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class HintInfoDTO
    {
        public HintInfoDTO(string hintText, bool hasNext)
        {
            HintText = hintText;
            HasNext = hasNext;
        }

        [JsonProperty("hint_text")] public string HintText { get; }
        [JsonProperty("has_next")] public bool HasNext { get; }
    }
}