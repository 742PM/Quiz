using System;
using System.Collections.Generic;

namespace Domain
{
    [Entity] //Should it be Entity?
    [MustBeSaved]
    public abstract class TaskGenerator : Entity<Guid>
    {
        public abstract  Task GetTask(Random randomSeed);


        /// <inheritdoc />
        protected TaskGenerator(Guid id) : base(id)
        {
        }
    }
}
