using Newtonsoft.Json;

namespace ComplexityWebApi.Controllers
{
    public class EditorDTO
    {
        public EditorDTO(string username, string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
        }

        [JsonProperty("username")] public string Username { get; }
        [JsonProperty("passwordHash")] public string PasswordHash { get; }
    }
}