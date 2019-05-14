using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var fillers = new List<IDatabaseFiller> { new ComplexityDatabaseFiller(), new HistoryDatabaseFiller() };
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
            var repository = CreateRepository(username, password);
            fillers.ForEach(r => r.Fill(repository));
        }

        private static MongoTaskRepository CreateRepository(string username, string password)
        {
            var db = MongoDatabaseInitializer.CreateMongoDatabase("ComplexityBot", username, password);
            return new MongoTaskRepository(db);
        }
    }
}