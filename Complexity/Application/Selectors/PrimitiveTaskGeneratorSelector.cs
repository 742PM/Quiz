using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories.Entities;
using Domain.Entities.TaskGenerators;

namespace Application.Selectors
{
    [Obsolete]
    public class PrimitiveTaskGeneratorSelector : ITaskGeneratorSelector
    {
        private int current;

        public TaskGenerator Select(IEnumerable<TaskGenerator> generators, UserProgressEntity progress)
        {
            var taskGenerators = generators.ToArray();
            var index = current % taskGenerators.Length;
            current++;
            return taskGenerators[index];
        }
    }
}