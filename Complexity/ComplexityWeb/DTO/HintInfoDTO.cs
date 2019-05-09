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

        [JsonProperty("hintText")] public string HintText { get; }
        [JsonProperty("hasNext")] public bool HasNext { get; }
    }
}