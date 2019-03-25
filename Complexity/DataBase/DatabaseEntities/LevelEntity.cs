namespace DataBase.DatabaseEntities
{
    public class LevelEntity
    {
        public string Description { get; set; }
        public GeneratorEntity[] Generators { get; set; }
    }
}