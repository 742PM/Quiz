using System;
using System.Linq;
using Infrastructure.Extensions;
using Scriban.Runtime;

namespace Domain.Entities.TaskGenerators
{
    public partial class TemplateLanguage
    {
        public const string Theta = "Θ";
        
        
        public const string Pow2 = "²";
        public const string Pow3 = "³";
        public const string Pow4 = "⁴";
        
        public const string Sqrt = "√";
        public const string Multiply = "∙";
        
        public static readonly string Theta1 = $"{Theta}(1)";
        public static readonly string ThetaN = $"{Theta}(n)";
        public static readonly string ThetaN2 = $"{Theta}(n{Pow2})";
        public static readonly string ThetaN3 = $"{Theta}(n{Pow3})";
        public static readonly string ThetaSqrtN = $"{Theta}({Sqrt}n)";
        public static readonly string ThetaLogN = $"{Theta}(log(n))";
        public static readonly string ThetaLog2N = $"{Theta}(log{Pow2}(n))";
        public static readonly string ThetaNLogN = $"{Theta}(n {Multiply} log(n))";
        public static readonly string ThetaLogNLogLogN = $"{Theta}(log(n) {Multiply} log(log(n)))";

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
            AddInstanceMethod<object, string>(GetLoopBody, random, so);
            AddInstanceMethod<object,string ,string>(GetLoopBodyWithDependentVar, random, so);
            AddMethod<int, int, int>(Random, random, so);
        }

        /// <summary>
        ///     EXAMPLE USAGE:
        ///         {{get_loop_body_with_dependent_var from1 iter3}}
        /// </summary>
        [ScriptMemberIgnore]
        public static object GetLoopBodyWithDependentVar(Random random, ScriptObject so, string first, string second)
        {
            return AnyOf(random, new ScriptArray(values: new[]
            {
$@"if ({first} % {so["iter1"]} == 0) 
    {second}++;",
$@"if ({first} % {so["iter1"]} == 0) 
    {second} += {first};",
$@"if ({first} > {so["iter1"]})
    {second} = {first};",
$@"if ({first} >= {so["iter1"]}) 
    {second} = {first};",
$@"if ({first} == {so["iter1"]}) 
    {second}++;",
            }.Select(x => x + "\t\t// ←  Θ(1)")));
        }
        /// <summary>
        ///     EXAMPLE USAGE:
        ///         {{get_loop_body from1}}
        /// </summary>
        [ScriptMemberIgnore]
        public static object GetLoopBody(Random random,ScriptObject so, string value) =>
            AnyOf(random, new ScriptArray(values: new[]
        {
$@"if ({value} % {so["iter1"]} == 0)
    Console.WriteLine({value});",
$@"if ({value} > {so["const5"]})
    Console.WriteLine({value});",
$@"if ({value} < {so["const5"]}) 
    Console.WriteLine({value});",
$@"if ({value} >= {so["const5"]}) 
    Console.WriteLine({value});",
$@"if ({value} <= {so["const5"]}) 
    Console.WriteLine({value});",
$@"if ({value} % {so["iter1"]} == 0) Console.WriteLine(""DEBUG!!!"");",
$@"if ({value} > {so["const5"]}) 
    Console.WriteLine({value}.ToString() + "" is more than {so["const5"]}"");",
$@"if ({value} < {so["const5"]})
    Console.WriteLine({value}.ToString() + "" is less than {so["const5"]}"");",
$@"Console.WriteLine({value})",
"Console.WriteLine(\"Hello User!\")",
"Console.WriteLine(\"Hello!\")",
"Console.WriteLine(\"Hi there!\")",
"Console.WriteLine(\"Hello there!\")",
$@"count += {so["from1"]};",
"count++;",
$@"c += {so["from1"]};",
"c++;"
            }.Select(x=>x+ "\t\t// ←  Θ(1)")));

        /// <summary>
        ///     You can define properties and fields that will be able to be used in templates;
        /// </summary>
        [ScriptMemberIgnore]
        public static int Random(Random random, int from, int to)
        {
            return random.Next(from, to);
        }

        /// <summary>
        ///     This is an example of a method that is possible to be used in templates;
        /// </summary>
        [ScriptMemberIgnore]
        public static object AnyOf(Random random, ScriptArray array)
        {
            return array[random.Next(array.Count)];
        }
    }
}
