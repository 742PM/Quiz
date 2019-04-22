using System.Collections.Generic;
using System.Linq;
using Application;
using Application.Selectors;
using Domain.Entities.TaskGenerators;

namespace Tests.Mocks
{
    public class TestTaskGeneratorSelector : ITaskGeneratorSelector
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
