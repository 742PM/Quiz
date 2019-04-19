namespace DataBase
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var filler = new DatabaseFiller();
            filler.Fill();
        }
    }
}