using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories.Entities;
using Domain.Entities.TaskGenerators;

namespace Application.Selectors
{
    public class RandomTaskGeneratorSelector : ITaskGeneratorSelector
    {
        private readonly Random random;

        public RandomTaskGeneratorSelector(Random random)
        {
            this.random = random;
        }

        public TaskGenerator Select(IEnumerable<TaskGenerator> generators, UserProgressEntity progress)
        {
            var generatorsArray = generators.ToArray();
            return generatorsArray.Length == 0 ? null : generatorsArray[random.Next(generatorsArray.Length)];
        }
    }
}