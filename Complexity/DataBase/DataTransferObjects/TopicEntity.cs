namespace DataBase
{
    public class TopicEntity
    {
        public int TopicId { get; set; }
        public bool IsCompleted { get; set; }
        
        public string Name { get; set; }
        public LevelEntity[] Levels { get; set; }
        public LevelEntity CurrentLevel { get; set; }
    }
}