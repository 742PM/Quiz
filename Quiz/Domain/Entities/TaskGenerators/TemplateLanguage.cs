using System;
using Scriban.Runtime;

namespace Domain.Entities.TaskGenerators
{
    public partial class TemplateLanguage
    {
        /// <summary>
        ///     You can define properties and fields that will be able to be used in templates;
        /// </summary>
        [ScriptMemberIgnore]
        public static int Random(Random random, int from, int to) => random.Next(from, to);

        /// <summary>
        ///     This method is going to be called in another part of the type.
        ///     Use <see cref="TemplateLanguage.AddMethod" /> to add methods you want to be part of language if they have some
        ///     randomness in it;
        ///     All desired methods should take <see cref="System.Random" /> as their first parameter;
        ///     Mark them with <see cref="ScriptMemberIgnoreAttribute" />
        /// </summary>
        /// <param name="random">You should pass it to <see cref="TemplateLanguage.AddMethod" /> </param>
        /// <param name="so">You should pass it to <see cref="TemplateLanguage.AddMethod" /></param>
        private static void AddMethods(Random random, ScriptObject so)
        {
            AddMethod<object, ScriptArray>(AnyOf, random, so);
            AddMethod<int, int, int>(Random, random, so);
        }

        /// <summary>
        ///     This is an example of a method that is possible to be used in templates;
        /// </summary>
        [ScriptMemberIgnore]
        public static object AnyOf(Random random, ScriptArray array) => array[random.Next(array.Count)];
    }
}