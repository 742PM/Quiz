namespace DataBase
{
    public class LevelDTO
    {
        public int LevelId { get; set; }
        public int AccessLevel { get; set; }
        public bool IsCompleted { get; set; }
        
        public TaskDTO CurrentTask { get; set; }
        public TaskDTO[] Tasks { get; set; }
    }
}