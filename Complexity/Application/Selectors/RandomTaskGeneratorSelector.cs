using System;
using System.Collections.Generic;
using System.Linq;
using Application.Extensions;
using Domain.Entities.TaskGenerators;
using Infrastructure.Result;

namespace Application.Selectors
{
    public class RandomTaskGeneratorSelector : ITaskGeneratorSelector
    {
        private readonly Random random;

        public RandomTaskGeneratorSelector(Random random)
        {
            this.random = random;
        }

        public Result<TaskGenerator, Exception> SelectGenerator(
            IEnumerable<TaskGenerator> generators,
            Dictionary<Guid, int> streaks)
        {
            var generatorsArray = generators as TaskGenerator[] ?? generators.ToArray();
            return generatorsArray.Length == 0 
                ? new ArgumentException($"{nameof(generators)} must be not empty") 
                : generatorsArray[random.Next(generatorsArray.Length)].Ok();
        }
    }
}