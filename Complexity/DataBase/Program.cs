
using System;
using MongoDB.Driver;

namespace DataBase
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var mongoConnectionString =
                Environment.GetEnvironmentVariable("COMPLEXITY_MONGO_CONNECTION_STRING")
                ?? "mongodb://localhost:27017";
            var db = new MongoClient(mongoConnectionString).GetDatabase("ComplexityBot");
            var userRepo = new MongoUserRepository(db);
        }
    }
}