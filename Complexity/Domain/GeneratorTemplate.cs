namespace Domain
{
    public class GeneratorTemplate
    {
        public string Code { get; set; }
        public string[] Hints { get; set; }
        public string Answer { get; set; }

        public void Deconstruct(out string code, out string[] hints, out string answer)
        {
            code = Code;
            hints = Hints;
            answer = Answer;
        }
    }
}