namespace Domain
{
    /// <summary>
    /// Subtopic for some topic
    /// </summary>
    public class Level
    {
        private readonly ITaskGeneratorSelector selector;

        public Level(ITaskGeneratorSelector selector, string description, (ITaskGenerator,int)[] generators)
        {
            Description = description;
            this.selector = selector;
            Generators = generators;
        }
        public string Description { get; }

        public Task GetTask( /* тут надо какое-то состояние юзера или чет вроде*/) =>
            selector.Select(Generators)
                    .GetTask(); 


        public (ITaskGenerator generator, int streak)[] Generators { get; }

    }
}