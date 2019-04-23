using System;
using System.Collections.Generic;
using System.Linq;
using Application.Selectors;
using Domain.Entities.TaskGenerators;

namespace Tests.Mocks
{
    public class TestTaskGeneratorSelector : ITaskGeneratorSelector
    {
        private int current;

        public TaskGenerator Select(IEnumerable<TaskGenerator> generators, Dictionary<Guid, int> streaks)
        {
            var taskGenerators = generators.ToArray();
            var index = current % taskGenerators.Length;
            current++;
            return taskGenerators[index];
        }
    }
}
