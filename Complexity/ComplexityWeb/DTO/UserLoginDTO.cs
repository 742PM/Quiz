using Newtonsoft.Json;

namespace ComplexityWebApi.DTO
{
    public class UserLoginDTO
    {
        public UserLoginDTO(string login, string token)
        {
            Login = login;
            Token = token;
        }

        [JsonProperty("description")] public string Login { get; }

        [JsonProperty("name")] public string Token { get; }
    }
}