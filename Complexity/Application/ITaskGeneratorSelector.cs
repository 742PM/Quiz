using System.Collections.Generic;
using Domain.Entities.TaskGenerators;

namespace Application
{
    public interface ITaskGeneratorSelector
    {
        TaskGenerator Select(IEnumerable<TaskGenerator> generators);
    }
}