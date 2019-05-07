using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
            var templateText = "{{var = 4}} || {{value_of_this}} {{random}} This is text; {{var}} " +
                               "is var and {{any_of [var, 1, 2,4,5,6,7,8,7]}} is func var || {%{any_of_r Random [1,2,'a',4,'c']}%}";
            //var templateText = "{{random}}";
            var template = Template.Parse(templateText);
            var r = new Random();
            var s = new ScriptObject();
            var so = new TemplateLanguage(r);
            s.Import("any_of", new Func<ScriptArray, object>((arr)=>arr[r.Next(arr.Count)]));
            s.Import(new C());
            s.Import((object)s);
            //so.Import(so);
            //so.Add("random", r);

            Console.WriteLine(template.Render(s));
            Console.WriteLine(new Func<int[],int>((arr)=>GetVar1(arr)).Method.Name);
        }

        public static bool Filter(MemberInfo info)
        {
            return true;
        }
        public static int Var => 7;

        public static int GetVar1(int[] xs)
        {
            return xs.Length + 3;
        }
    }
}
