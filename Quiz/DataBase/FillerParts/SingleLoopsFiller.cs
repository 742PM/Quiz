using System;
using System.Collections.Generic;
using Domain.Entities.TaskGenerators;

namespace DataBase
{
    internal partial class ReworkedComplexityFiller
    {
        public IEnumerable<TaskGenerator> CreateSingleLoops()
        {
            yield return new TemplateTaskGenerator(
                                                   Guid.NewGuid(),
                                                   StandardLoopAnswers,
                                                   GetForLoop() +
                                                   SimpleOperation,
                                                   new string[0],
                                                   ThetaN, 1, Question);

            yield return new TemplateTaskGenerator(
                                                   Guid.NewGuid(),
                                                   StandardLoopAnswers,
                                                   GetForLoop(increment: MultiplyEqual) +
                                                   SimpleOperation,
                                                   new[] { "Цикл с удвоением" },
                                                   ThetaLogN, 2, Question);

            yield return new TemplateTaskGenerator(
                                                   Guid.NewGuid(),
                                                   new[] { ThetaLogN, Theta1, ThetaN, ThetaNLogN, ThetaN2 },
                                                   GetForLoop(
                                                              comparision: $"{OuterIterable} < {OuterTo} * {OuterTo}",
                                                              increment: MultiplyEqual) +
                                                   SimpleOperation,
                                                   new[] { "Свойства логарифма" },
                                                   ThetaLogN, 3, Question);

            yield return new TemplateTaskGenerator(
                                                   Guid.NewGuid(),
                                                   StandardLoopAnswers,
                                                   GetForLoop(comparision:
                                                              $"{OuterIterable} * {OuterIterable} < {OuterTo}") +
                                                   SimpleOperation,
                                                   new string[0],
                                                   ThetaSqrtN, 2, Question);

            yield return new TemplateTaskGenerator(
                                                   Guid.NewGuid(),
                                                   StandardLoopAnswers,
                                                   GetForLoop(comparision:
                                                              $"{OuterIterable} < {OuterTo} * {OuterTo}") +
                                                   SimpleOperation,
                                                   new string[0],
                                                   ThetaN2, 2, Question);

            yield return new TemplateTaskGenerator(
                                                   Guid.NewGuid(),
                                                   StandardLoopAnswers,
                                                   GetForLoop(comparision: $"{OuterIterable} % {OuterFrom} != 0") +
                                                   SimpleOperation,
                                                   new string[0],
                                                   Theta1, 2, Question);
        }
    }
}