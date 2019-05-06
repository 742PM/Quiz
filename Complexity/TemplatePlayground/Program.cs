using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Infrastructure;
using Scriban;
using Scriban.Runtime;

namespace TemplatePlayground
{

    class Program
    {
        static void Main(string[] args)
        {
            var templateText = "{{var = 4}}This is text; {{var}} is var and {{any_of [var, 1, 2,4,5,6,7,8,7]}} is func var || {{any_of_r random [1,2,'a',4,'c']}}";
            //var templateText = "{{random}}";
            var template = Template.Parse(templateText, templateText);
            var r = new Random();
            var so = new TemplateLanguage(r);
            so.Import(so);
            so.Add("random", r);
            
            Console.WriteLine(template.Render(so));
        }

        public static int Var => 7;

        public static int GetVar1(int[] xs)
        {
            return xs.Length + 3;
        }
    }

    public class C
    {
        public Random Random => new Random(42);
    }
    public class TemplateLanguage : ScriptObject
    {
        public TemplateLanguage(Random random)
        {
            Random = random;
        }

        private static readonly Random TrueRandom = new Random();

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

    public class Templator : ScriptObject
    {
        
        public static int GetVar(ScriptArray xs)
        {
            return (int)xs[0];
        }
    }
}
