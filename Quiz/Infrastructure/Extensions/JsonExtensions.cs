using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Extensions
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerSettings ThetaSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver {NamingStrategy = new SnakeCaseNamingStrategy()},
            Formatting = Formatting.Indented
        };

        public static string Serialize<T>(this T item) => JsonConvert.SerializeObject(item, ThetaSettings);

        public static T Deserialize<T>(this string item) => JsonConvert.DeserializeObject<T>(item, ThetaSettings);
    }
}