namespace DataBase.DatabaseEntities
{
    public class LevelEntity
    {
        public string Description { get; set; }
        public IGeneratorEntity[] Generators { get; set; }
    }
}