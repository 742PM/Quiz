namespace DataBase
{
    public class LevelEntity
    {
        public int LevelId { get; set; }
        public int AccessLevel { get; set; }
        public bool IsCompleted { get; set; }
        
        public TaskEntity CurrentTask { get; set; }
        public TaskEntity[] Tasks { get; set; }
    }
}