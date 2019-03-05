namespace DataBase
{
    public class Topic
    {
        public int TopicId { get; set; }
        public bool IsCompleted { get; set; }
        
        public string Name { get; set; }
        public Level[] Levels { get; set; }
        public Level CurrentLevel { get; set; }
    }
}