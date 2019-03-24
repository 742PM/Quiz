namespace Domain
{
    /// <summary>
    /// Subtopic for some topic
    /// </summary>
    [MustBeSaved]
    [Value] //Should it be Entity?
    public class Level
    {
        public Level( string description, (TaskGenerator,int)[] generators)
        {
            Description = description;
            Generators = generators;
        }

        public string Description { get; }

        public (TaskGenerator generator, int streak)[] Generators { get; }

    }
}