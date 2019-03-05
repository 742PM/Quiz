namespace DataBase
{
    public class Excercise
    {
        public int ExcerciseId { get; set; }
        public string GeneratorId { get; set; }
        public string ExpectedAnswer { get; set; }
        public string[] Variants { get; set; }
        public string Text { get; set; }
    }
}