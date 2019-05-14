using System;

namespace DataBase
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var filler = new DatabaseFiller();
            var username = Environment.GetEnvironmentVariable("MONGO_USERNAME");
            if (username == null)
            {
                Console.Write("Username: ");
                username = Console.ReadLine();
            }
            var password = Environment.GetEnvironmentVariable("MONGO_PASSWORD");
            if (password == null)
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
            }
            filler.Fill(username, password);
        }
    }
}