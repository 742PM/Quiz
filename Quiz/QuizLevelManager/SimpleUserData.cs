namespace QuizLevelManager
{
    public class SimpleUserData
    {
        public SimpleUserData(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; }
        public string Password { get; }
    }
}