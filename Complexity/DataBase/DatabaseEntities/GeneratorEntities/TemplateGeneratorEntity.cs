namespace DataBase.DatabaseEntities.GeneratorEntities
{
    public class TemplateGeneratorEntity : IGeneratorEntity
    {
        public int Streak { get; set; }
        public string Description { get; set; }
        
        public string Code { get; set; }
        public string Answer { get; set; }
        public string[] Hints { get; set; }
    }
}