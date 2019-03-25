using System;
using Domain.Values;

namespace Domain.Entities.TaskGenerators
{
    [Entity]
    [MustBeSaved]
    public abstract class TaskGenerator : Entity<Guid>
    {
        /// <inheritdoc />
        protected TaskGenerator(Guid id) : base(id)
        {
        }

        public abstract Task GetTask(Random randomSeed);
    }
}
