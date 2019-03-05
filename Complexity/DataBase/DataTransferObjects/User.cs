namespace DataBase
{
    public class User
    {
        public int UserId { get; set; }
        public Level[] completedLevels { get; set; }
        public Level currentLevel { get; set; }
    }
}