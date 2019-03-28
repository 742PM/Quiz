using System;
using System.Collections.Generic;
using DataBase.Entities;
using Domain.Entities.TaskGenerators;
using MongoDB.Driver;

namespace DataBase
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var mongoConnectionString = Environment.GetEnvironmentVariable("COMPLEXITY_MONGO_CONNECTION_STRING") ??
                                        "mongodb://localhost:27017";
            var db = new MongoClient(mongoConnectionString).GetDatabase("ComplexityBot");
            var userRepo = SetupDatabase(db);

            var levelProgressEntity = new LevelProgressEntity
            {
                LevelId = Guid.NewGuid(), CurrentLevelStreaks = new Dictionary<Guid, int> {{Guid.NewGuid(), 10}}
            };

            var topicProgressEntity = new TopicProgressEntity
            {
                TopicId = Guid.NewGuid(),
                LevelProgressEntities = new Dictionary<Guid, LevelProgressEntity>
                {
                    {Guid.NewGuid(), levelProgressEntity}
                }
            };

            var userProgress = new UserProgressEntity
            {
                UserId = Guid.NewGuid(), CurrentLevelId = Guid.NewGuid(), CurrentTopicId = Guid.NewGuid(),
                TopicsProgress = new Dictionary<Guid, TopicProgressEntity> {{Guid.NewGuid(), topicProgressEntity}}
            };

            var user = new UserEntity(Guid.NewGuid(), userProgress);
            var t = new TemplateTaskGenerator(Guid.Empty, new[] {"12a"}, "for i in j", new string[0], "woah", 1);
            var tasks = db.GetCollection<TaskGenerator>("tasks");
            Console.WriteLine(t.Id);
            tasks.InsertOne(t);
            Console.WriteLine(t.Id);

            var taskGenerators = tasks.FindSync(f => f.Streak == 1)
                                      .Current;
            Console.Write(taskGenerators);
            userRepo.Insert(user);
        }

        private static MongoUserRepository SetupDatabase(IMongoDatabase db)
        {
            MongoDatabaseInitializer.SetupDatabase();
            var userRepo = new MongoUserRepository(db);

            return userRepo;
        }
    }
}
