using LiteDB;

namespace DataBase
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using(var db = new LiteDatabase(@"MyData.db"))
            {
                var customers = db.GetCollection<User>("users");

                var customer = new User
                { 
                    UserId = 1,
                    completedLevels = new[]{new Level()},
                    currentLevel = new Level()
                    
                };

                customers.Insert(customer);
            }
        }
    }
}