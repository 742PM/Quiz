using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.TaskGenerators;

namespace Application.Selectors
{
    public class ProgressTaskGeneratorSelector : ITaskGeneratorSelector
    {
        private readonly Random random;

        public ProgressTaskGeneratorSelector(Random random)
        {
            this.random = random;
        }

        public TaskGenerator Select(IEnumerable<TaskGenerator> generators, Dictionary<Guid, int> streaks)
        {
            var variants = new List<TaskGenerator>();
            foreach (var generator in generators)
            {
                var leftForStreak = generator.Streak - streaks[generator.Id];
                for (var i = 0; i < leftForStreak; i++)
                    variants.Add(generator);
            }

            return variants[random.Next(variants.Count)];
        }
    }
}