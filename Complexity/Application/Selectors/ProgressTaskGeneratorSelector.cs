using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.TaskGenerators;

namespace Application.Selectors
{
    public class ProgressTaskGeneratorSelector : ITaskGeneratorSelector
    {
        private readonly Random random;
        private readonly ITaskGeneratorSelector alternativeSelector;

        public ProgressTaskGeneratorSelector(Random random, ITaskGeneratorSelector alternativeSelector)
        {
            this.random = random;
            this.alternativeSelector = alternativeSelector;
        }

        public TaskGenerator Select(IEnumerable<TaskGenerator> generators, Dictionary<Guid, int> streaks)
        {
            var generatorsArray = generators as TaskGenerator[] ?? generators.ToArray();

            var variants = new List<TaskGenerator>();
            foreach (var generator in generatorsArray)
            {
                var leftForStreak = generator.Streak - streaks[generator.Id];
                for (var i = 0; i < leftForStreak; i++)
                    variants.Add(generator);
            }

            return variants.Count > 0
                ? variants[random.Next(variants.Count)]
                : alternativeSelector.Select(generatorsArray, streaks);
        }
    }
}