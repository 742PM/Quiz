using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.TaskGenerators;

namespace Application
{
    [Obsolete]
    public class PrimitiveTaskGeneratorSelector : ITaskGeneratorSelector
    {
        private int current;

        public TaskGenerator Select(IEnumerable<TaskGenerator> generators)
        {
            var taskGenerators = generators.ToArray();
            var index = current % taskGenerators.Length;
            current++;
            return taskGenerators[index];
        }
    }
}