using System;
using Domain.Values;

namespace Domain.Entities.TaskGenerators
{
    /// <summary>
    /// Base class for all TaskGenerators; One should use derived ones;
    /// </summary>
    [Entity]
    [MustBeSaved]
//  [BsonDiscriminator(RootClass = true)]
//  [BsonKnownTypes(typeof(TemplateTaskGenerator))]
// TODO: research with issue about outer serialization
    public class TaskGenerator : Entity<Guid>
    {
        /// <inheritdoc />
        protected TaskGenerator(Guid id, int streak) : base(id)
        {
            Streak = streak;
        }
        public int Streak { get; }


        public Task GetTask(Random randomSeed) => throw new NotSupportedException("You can not use this class. Use derived ones;");
    }
}
