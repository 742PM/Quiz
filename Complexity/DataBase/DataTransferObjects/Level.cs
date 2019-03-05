namespace DataBase
{
    public class Level
    {
        public int LevelId { get; set; }
        public bool IsCompleted { get; set; }
        
        public Excercise CurrentExcercise { get; set; }
        public Generators LevelGenerators { get; set; }
    }
}