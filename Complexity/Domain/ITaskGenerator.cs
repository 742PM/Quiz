using System;
using System.Collections.Generic;

namespace Domain
{
    public interface ITaskGenerator
    {
        IEnumerable<Task> Tasks { get; }

        string Description { get; }

        int Difficulty { get; }

        Guid Id { get; }
    }
}
