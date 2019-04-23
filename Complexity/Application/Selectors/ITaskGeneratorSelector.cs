using System;
using System.Collections.Generic;
using Domain.Entities.TaskGenerators;

namespace Application.Selectors
{
    public interface ITaskGeneratorSelector
    {
        TaskGenerator Select(IEnumerable<TaskGenerator> generators, Dictionary<Guid, int> streaks);
    }
}
