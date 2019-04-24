using System;
using System.Collections.Generic;
using System.Linq;
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

        public TaskGenerator SelectGenerator(IEnumerable<TaskGenerator> generators, Dictionary<Guid, int> streaks)
        {
            var generatorsArray = generators as TaskGenerator[] ?? generators.ToArray();
            return generatorsArray.Length == 0 ? null : generatorsArray[random.Next(generatorsArray.Length)];
                // не уверен насчет null возможно стоит еще подумать над этим
        }
    }
}