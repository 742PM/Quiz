using System;
using Scriban.Runtime;

namespace Domain.Entities.TaskGenerators
{
    public partial class TemplateLanguage
    {
        private TemplateLanguage(Random random)
        {
            Random = random;
        }

        public static ScriptObject Create(Random random)
        {
            var language = new TemplateLanguage(random);
            var so = new ScriptObject();
            AddMethod<object, ScriptArray>(AnyOf, random, so);
            so.Import(language);
            return so;
        }

        private static void AddMethod<TOut, TIn>(Func<Random, TIn, TOut> method, Random random, ScriptObject so)
        {
            so.Import(method.Method.Name, new Func<TIn, TOut>(item => method(random, item)));
        }
    }
}