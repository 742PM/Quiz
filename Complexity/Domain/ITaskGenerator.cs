using System;
using System.Collections.Generic;

namespace Domain
{
    public interface ITaskGenerator : IEnumerable<Task>
    {
        Task GetTask(Random randomSeed);
        Task GetTask();

        string Description { get; }

        int Difficulty { get; } //нужно удалить нахер

        Guid Id { get; }
    }
}
