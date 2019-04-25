using System;
using System.Collections.Generic;
using Domain.Entities.TaskGenerators;
using Infrastructure.Result;

namespace Application.Selectors
{
    public interface ITaskGeneratorSelector
    {
        Result<TaskGenerator, Exception> SelectGenerator(
            IEnumerable<TaskGenerator> generators,
            Dictionary<Guid, int> streaks);
    }
}