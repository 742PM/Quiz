using System;
using System.Collections.Generic;
using Scriban.Runtime;

namespace Domain.Entities.TaskGenerators
{
    public partial class TemplateLanguage
    {
        public const string LoopVariable = "loop_var";
        public const string Const = "const";
        public const string From = "from";
        public const string To = "to";
        public const string SimpleOperation = "simple_operation";
        public const string IterateConstant = "iter";
        public const int LoopAmount = 8;
        public const int MaxRandomConstantValue = 50;
        public const int BaseTemplateKeywordsAmount = 5;

        public static readonly IReadOnlyCollection<string> LoopVariables = new[]
                                                                           {"i", "j", "k", "x", "y", "step"};

        public static readonly IReadOnlyCollection<string> Tos = new[] { "n", "m", "length", "amount", "size" };

        public static readonly IReadOnlyCollection<string> Operations = new[]
                                                                        {"c++", "k1--", "service.Update()", "var a = Environment.GetVariable(\"VAR\")", "k3++"};

        /// <summary>
        ///     You can define properties and fields that will be able to be used in templates;
        /// </summary>
        public static int Random(Random random, int from, int to)
        {
            return random.Next(from, to);
        }

        /// <summary>
        ///     This method is going to be called in another part of the type.
        ///     Use <see cref="TemplateLanguage.AddMethod" /> to add method you want to be part of language;
        /// </summary>
        /// <param name="random">You should pass it to <see cref="TemplateLanguage.AddMethod" /> </param>
        /// <param name="so">You should pass it to <see cref="TemplateLanguage.AddMethod" /></param>
        private static void AddMethods(Random random, ScriptObject so)
        {
            AddMethod<object, ScriptArray>(AnyOf, random, so);
            AddMethod<int, int,int>(Random, random, so);
        }

        /// <summary>
        ///     This is an example of a method that is possible to be used in templates;
        /// </summary>
        public static object AnyOf(Random random, ScriptArray array)
        {
            return array[random.Next(array.Count)];
        }
    }
}