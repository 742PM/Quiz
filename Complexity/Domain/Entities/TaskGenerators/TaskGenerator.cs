using System;
using Domain.Values;

namespace Domain.Entities.TaskGenerators
{
    [Entity]
    [MustBeSaved]
//    [BsonDiscriminator(RootClass = true)]
//    [BsonKnownTypes(typeof(TemplateGeneratorEntity))]
// TODO: research with issue about outer serialization
    public abstract class TaskGenerator : Entity<Guid>
    {
        /// <inheritdoc />
        protected TaskGenerator(Guid id, int streak, string description) : base(id)
        {
            Streak = streak;
            Description = description;
        }
        public int Streak { get; }

        public string Description { get; }

        public abstract Task GetTask(Random randomSeed);
    }
}
