using System;
using Infrastructure;
using Scriban.Runtime;

namespace Domain.Entities.TaskGenerators
{
    public partial class TemplateLanguage 
    {
        public Random Random { get; }

        public static object AnyOf(Random random, ScriptArray array)
        {
            return array[random.Next(array.Count)];
        }
    }
}