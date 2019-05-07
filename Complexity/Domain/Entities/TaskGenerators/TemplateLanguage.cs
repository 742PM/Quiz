using System;
using Scriban.Runtime;

namespace Domain.Entities.TaskGenerators
{
    public partial class TemplateLanguage
    {
        /// <summary>
        ///     You can define properties and fields that will be able to be used in templates;
        /// </summary>
        public Random Random { get; }

        /// <summary>
        ///     This method is going to be called in another part of the type.
        ///     Use <see cref="TemplateLanguage.AddMethod" /> to add method you want to be part of language;
        /// </summary>
        /// <param name="random">You should pass it to <see cref="TemplateLanguage.AddMethod" /> </param>
        /// <param name="so">You should pass it to <see cref="TemplateLanguage.AddMethod" /></param>
        public static void AddMethods(Random random, ScriptObject so)
        {
            AddMethod<object, ScriptArray>(AnyOf, random, so);
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