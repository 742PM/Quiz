using System;
using Scriban.Runtime;

namespace TemplatePlayground
{
    public class TemplateLanguage 
    {
        public TemplateLanguage(Random random)
        {
            Random = random;
            //this.Add(nameof(Random).ToUnderscoreCase(), random);
        }

        private static readonly Random TrueRandom = new Random();
        public string GetTo(int x) => $"{x+9000}";
        public Random ValueOfThis { get; }

        public Random Random { get; }

        public static object AnyOf(ScriptArray array)
        {
            return array[TrueRandom.Next(array.Count)];
        }
        public static object AnyOfR(Random random, ScriptArray array)
        {
            return array[random.Next(array.Count)];
        }

    }
}