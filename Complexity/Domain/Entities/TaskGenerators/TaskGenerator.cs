using System;
using Domain.Values;

namespace Domain.Entities.TaskGenerators
{
    [Entity]
    [MustBeSaved]
//  [BsonDiscriminator(RootClass = true)]
//  [BsonKnownTypes(typeof(TemplateTaskGenerator))]
// TODO: research with issue about outer serialization
    public abstract class TaskGenerator : Entity<Guid>
    {
        /// <inheritdoc />
        protected TaskGenerator(Guid id, int streak) : base(id)
        {
            Streak = streak;
        }
        public int Streak { get; }


        public abstract Task GetTask(Random randomSeed);
    }
}
