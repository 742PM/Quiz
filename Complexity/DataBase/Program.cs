using System;

namespace DataBase
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var filler = new DatabaseFiller();
            var username = Console.ReadLine();
            var password = Console.ReadLine();
            filler.Fill(username, password);
        }
    }
}