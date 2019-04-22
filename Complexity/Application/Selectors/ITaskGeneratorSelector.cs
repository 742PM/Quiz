using System.Collections.Generic;
using Application.Repositories.Entities;
using Domain.Entities.TaskGenerators;

namespace Application.Selectors
{
    public interface ITaskGeneratorSelector
    {
        TaskGenerator Select(IEnumerable<TaskGenerator> generators, UserProgressEntity progress);
    }
}
