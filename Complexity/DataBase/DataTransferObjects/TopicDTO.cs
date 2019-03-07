namespace DataBase
{
    public class TopicDTO
    {
        public int TopicId { get; set; }
        public bool IsCompleted { get; set; }
        
        public string Name { get; set; }
        public LevelDTO[] Levels { get; set; }
        public LevelDTO CurrentLevel { get; set; }
    }
}