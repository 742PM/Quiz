using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Entities.TaskGenerators;

namespace DataBase
{
    public class ComplexityDatabaseFiller : IDatabaseFiller
    {
        private const string Theta = "Θ";
        private const string Sqrt = "√";
        private const string Pow2 = "²";
        private const string Pow3 = "³";
        private const string Multiply = "∙";

        private const string Question = "Оцените временную сложность данного алгоритма:";

        private const string OuterIterable = "{{loop_var1}}";
        private const string InnerIterable = "{{loop_var2}}";
        private const string OuterFrom = "{{from1}}";
        private const string InnerFrom = "{{from2}}";
        private const string OuterTo = "{{to1}}";
        private const string InnerTo = "{{to2}}";
        private const string OuterIteration = "{{iter1}}";
        private const string InnerIteration = "{{iter2}}";

        private const string SimpleOperation = "\t{{simple_operation1}}";
        private const string PlusEqual = "+=";
        private const string MultiplyEqual = "*=";

        private static readonly string Theta1 = $"{Theta}(1)";
        private static readonly string ThetaN = $"{Theta}(n)";
        private static readonly string ThetaN2 = $"{Theta}(n{Pow2})";
        private static readonly string ThetaN3 = $"{Theta}(n{Pow3})";
        private static readonly string ThetaSqrtN = $"{Theta}({Sqrt}n)";
        private static readonly string ThetaLogN = $"{Theta}(log(n))";
        private static readonly string ThetaLog2N = $"{Theta}(log{Pow2}(n))";
        private static readonly string ThetaNLogN = $"{Theta}(n {Multiply} log(n))";
        private static readonly string ThetaLogNLogLogN = $"{Theta}(log(n) {Multiply} log(log(n)))";

        private static readonly string[] StandardLoopAnswers = { ThetaLogN, ThetaSqrtN, Theta1, ThetaN, ThetaN2 };
        private static readonly string[] StandardDoubleAnswers = { ThetaN, ThetaNLogN, ThetaN2, ThetaN3 };
        private static readonly string[] DifficultDoubleAnswers = { ThetaLog2N, ThetaN, ThetaNLogN, ThetaN2 };

        public void Fill(MongoTaskRepository repository)
        {
            var topic = new Topic(Guid.NewGuid(), "Сложность алгоритмов",
                "Описание: Задачи на разные алгоритмы и разные сложности", new Level[0]);
            var singleLoopLevels = new Level(Guid.NewGuid(), "Циклы", new TaskGenerator[0], new Guid[0]);
            var doubleLoopLevels = new Level(Guid.NewGuid(), "Двойные Циклы", new TaskGenerator[0], new Guid[0]);

            repository.InsertTopic(topic);
            repository.InsertLevel(topic.Id, singleLoopLevels);
            repository.InsertLevel(topic.Id, doubleLoopLevels);

            var forLoop1 = new TemplateTaskGenerator(Guid.NewGuid(),
                StandardLoopAnswers,
                GetForLoop() +
                SimpleOperation,
                new string[0], ThetaN, 1,Question);

            var forLoop2 = new TemplateTaskGenerator(Guid.NewGuid(),
                StandardLoopAnswers,
                GetForLoop(increment: MultiplyEqual) +
                SimpleOperation,
                new[] { "Цикл с удвоением" }, ThetaLogN, 1, Question);

            var forLoop3 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] { ThetaLogN, Theta1, ThetaN, ThetaNLogN, ThetaN2 },
                GetForLoop(comparision: $"{OuterIterable} < {OuterTo} * {OuterTo}", increment: MultiplyEqual) +
                SimpleOperation,
                new[] { "Свойства логарифма" }, ThetaLogN, 1, Question);

            var forLoop4 = new TemplateTaskGenerator(Guid.NewGuid(),
                StandardLoopAnswers,
                GetForLoop(comparision: $"{OuterIterable} * {OuterIterable} < {OuterTo}") +
                SimpleOperation,
                new string[0], ThetaSqrtN, 1, Question);

            var forLoop5 = new TemplateTaskGenerator(Guid.NewGuid(),
                StandardLoopAnswers,
                GetForLoop(comparision: $"{OuterIterable} < {OuterTo} * {OuterTo}") +
                SimpleOperation,
                new string[0], ThetaN2, 1, Question);

            var forLoop6 = new TemplateTaskGenerator(Guid.NewGuid(),
                StandardLoopAnswers,
                GetForLoop(comparision: $"{OuterIterable} % {OuterFrom} != 0") +
                SimpleOperation,
                new string[0], Theta1, 1, Question);

            repository.InsertGenerators(topic.Id, singleLoopLevels.Id,
                new[]
                {
                    forLoop1,
                    forLoop2,
                    forLoop3,
                    forLoop4,
                    forLoop5,
                    forLoop6
                });

            var double1 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] { ThetaN, ThetaN2, ThetaN3 },
                GetForLoop() +
                GetInnerForLoop() +
                "\t" + SimpleOperation,
                new[] { "Независимые циклы" }, ThetaN2, 1, Question);

            var double2 = new TemplateTaskGenerator(Guid.NewGuid(),
                StandardDoubleAnswers,
                GetForLoop() +
                GetInnerForLoop(increment: MultiplyEqual) +
                "\t" + SimpleOperation,
                new[] { "Независимые циклы" }, ThetaNLogN, 1, Question);

            var double3 = new TemplateTaskGenerator(Guid.NewGuid(),
                StandardDoubleAnswers,
                GetForLoop() +
                GetInnerForLoop(incrementValue: OuterIterable) +
                "\t" + SimpleOperation,
                new[] { "Частичная сумма гармонического ряда" }, ThetaNLogN, 1, Question);

            var double4 = new TemplateTaskGenerator(Guid.NewGuid(),
                StandardDoubleAnswers,
                GetForLoop() +
                GetInnerForLoop(increment: MultiplyEqual, incrementValue: "i") +
                "\t" + SimpleOperation,
                new[] { $"log(n) {Multiply} li(n) = log(n) {Multiply} n / log(n) = n" }, ThetaN, 1, Question);

            var double5 = new TemplateTaskGenerator(Guid.NewGuid(),
                StandardDoubleAnswers,
                GetForLoop() +
                GetInnerForLoop(OuterIterable) +
                "\t" + SimpleOperation,
                new[] { "Арифметическая прогрессия" }, ThetaN2, 1, Question);

            var double6 = new TemplateTaskGenerator(Guid.NewGuid(),
                StandardDoubleAnswers,
                GetForLoop() +
                GetInnerForLoop(OuterIterable, MultiplyEqual) +
                "\t" + SimpleOperation,
                new[] { "Логарифм факториала, Формула Стирлинга" }, ThetaNLogN, 1, Question);

            var double7 = new TemplateTaskGenerator(Guid.NewGuid(),
                StandardDoubleAnswers,
                GetForLoop(increment: MultiplyEqual) +
                GetInnerForLoop(OuterIterable) +
                "\t" + SimpleOperation,
                new[] { "Независимые циклы" }, ThetaNLogN, 1, Question);

            var double8 = new TemplateTaskGenerator(Guid.NewGuid(),
                DifficultDoubleAnswers,
                GetForLoop(increment: MultiplyEqual) +
                GetInnerForLoop(OuterIterable, MultiplyEqual) +
                "\t" + SimpleOperation,
                new[] { "Независимые циклы" }, ThetaLog2N, 1, Question);

            var double9 = new TemplateTaskGenerator(Guid.NewGuid(),
                DifficultDoubleAnswers,
                GetForLoop(increment: MultiplyEqual) +
                GetInnerForLoop(OuterIterable) +
                "\t" + SimpleOperation,
                new[] { "Геометрическая прогрессия" }, ThetaN, 1, Question);

            var double10 = new TemplateTaskGenerator(Guid.NewGuid(),
                new[] { ThetaLogNLogLogN, ThetaN, ThetaNLogN },
                GetForLoop(increment: MultiplyEqual) +
                GetInnerForLoop(OuterIterable, MultiplyEqual, OuterIterable) +
                "\t" + SimpleOperation,
                new[] { "Cмена основания логарифма и частичная сумма гармонического ряда" }, ThetaLogNLogLogN, 1, Question);

            var double11 = new TemplateTaskGenerator(Guid.NewGuid(),
                DifficultDoubleAnswers,
                GetForLoop(increment: MultiplyEqual) +
                GetInnerForLoop(OuterIterable, MultiplyEqual) +
                "\t" + SimpleOperation,
                new[] { "Арифметическая прогрессия" }, ThetaLog2N, 1, Question);

            repository.InsertGenerators(topic.Id, doubleLoopLevels.Id,
                new[]
                {
                    double1,
                    double2,
                    double3,
                    double4,
                    double5,
                    double6,
                    double7,
                    double8,
                    double9,
                    double10,
                    double11
                });
        }

        private static string GetForLoop(
            string iterable = OuterIterable,
            string from = OuterFrom,
            string comparision = OuterIterable + " < " + OuterTo,
            string increment = PlusEqual,
            string incrementValue = OuterIteration) =>
            $"for (var {iterable} = {from}; {comparision}; {iterable} {increment} {incrementValue})\n";

        private static string GetInnerForLoop(
            string upperBound = InnerTo,
            string increment = PlusEqual,
            string incrementValue = InnerIteration) =>
            $"\t{GetForLoop(InnerIterable, InnerFrom, $"{InnerIterable} < {upperBound}", increment, incrementValue)}";
    }
}