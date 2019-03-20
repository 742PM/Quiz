using System;
using System.Collections.Generic;

namespace Domain
{
    public interface ITaskGenerator
    {
        Task GetTask(Random randomSeed);

        string Description { get; }

        int Difficulty { get; }

        Guid Id { get; }
    }
}
