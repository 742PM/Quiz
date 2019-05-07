using Scriban.Runtime;

namespace TemplatePlayground
{
    public class Templator : ScriptObject
    {
        
        public static int GetVar(ScriptArray xs)
        {
            return (int)xs[0];
        }
    }
}