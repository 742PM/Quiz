using System;
using System.Collections.Generic;

namespace Domain
{
    [Value] //Should it be Entity?
    [MustBeSaved]
    public abstract class TaskGenerator
    {
        public abstract  Task GetTask(Random randomSeed);

        [MustBeSaved]
        public abstract Guid Id { get; }
    }
}
