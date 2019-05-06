using System;
using System.Runtime.CompilerServices;
using Scriban;
using Scriban.Runtime;

namespace TemplatePlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var templateText = "{{var = 4}}This is text; {{var}} is var and {{get_var var}} is func var";
            var template = Template.Parse(templateText, templateText);
            var so = new Templator();
            var r = new Random(3);
            so.Add("random", r);
            
            Console.WriteLine(template.Render(so));
        }

        public static int Var => 7;

        public static int GetVar1(int i)
        {
            return i + 3;
        }
    }

    public static class ScribanExtensions
    {

    }
    public class Templator : ScriptObject
    {
        public static int GetVar(int i)
        {
            return i + 3;
        }
        public static int GetVar(int i, Random random)
        {
            return i + random.Next(100, 1001);
        }
    }
}
