using System;
using System.Collections.Generic;

namespace Domain
{
    //мб надо будет переименовать
    public interface ILevel
    {
        IEnumerable<(IGenerator, int)> GeneratorsWithCountsToSolve { get; }
        string Description { get; }
        IGenerator GetRandomGenerator(Random rnd);
    }
}