namespace DataBase
{
    internal partial class ReworkedComplexityFiller : IDatabaseFiller
    {
        private const string Theta = "Θ";
        private const string Sqrt = "√";
        private const string Pow2 = "²";
        private const string Pow3 = "³";
        private const string Multiply = "∙";

        internal const string Question = "Оцените временную сложность данного алгоритма:";

        private const string OuterIterable = "{{loop_var1}}";
        private const string InnerIterable = "{{loop_var2}}";
        private const string OuterFrom = "{{from1}}";
        private const string InnerFrom = "{{from2}}";
        private const string OuterTo = "{{to1}}";
        private const string InnerTo = "{{to2}}";
        private const string OuterIteration = "{{iter1}}";
        private const string InnerIteration = "{{iter2}}";

        internal const string SimpleOperation = "\t{{simple_operation1}}";
        private const string PlusEqual = "+=";
        private const string MultiplyEqual = "*=";

        private static readonly string Theta1 = $"{Theta}(1)";
        internal static readonly string ThetaN = $"{Theta}(n)";
        private static readonly string ThetaN2 = $"{Theta}(n{Pow2})";
        private static readonly string ThetaN3 = $"{Theta}(n{Pow3})";
        private static readonly string ThetaSqrtN = $"{Theta}({Sqrt}n)";
        internal static readonly string ThetaLogN = $"{Theta}(log(n))";
        private static readonly string ThetaLog2N = $"{Theta}(log{Pow2}(n))";
        private static readonly string ThetaNLogN = $"{Theta}(n {Multiply} log(n))";
        private static readonly string ThetaLogNLogLogN = $"{Theta}(log(n) {Multiply} log(log(n)))";

        internal static readonly string[] StandardLoopAnswers = { ThetaLogN, ThetaSqrtN, Theta1, ThetaN, ThetaN2 };
        private static readonly string[] StandardDoubleAnswers = { ThetaN, ThetaNLogN, ThetaN2, ThetaN3 };
        private static readonly string[] DifficultDoubleAnswers = { ThetaLog2N, ThetaN, ThetaNLogN, ThetaN2 };

        public void Fill(MongoTaskRepository repository)
        {
            //repository.Add(new Level(... singleLoops);
        }

        internal static string GetForLoop(
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