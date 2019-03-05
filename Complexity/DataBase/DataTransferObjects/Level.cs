namespace DataBase
{
    public class Level
    {
        public int LevelId { get; set; }
        public int AccessLevel { get; set; }
        public bool IsCompleted { get; set; }
        
        public Task CurrentTask { get; set; }
        public Task[] Tasks { get; set; }
    }
}