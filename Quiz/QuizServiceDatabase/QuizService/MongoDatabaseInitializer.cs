using System;
using Application.Repositories.Entities;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using Infrastructure;
using Infrastructure.DDD;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace QuizServiceDatabase.QuizService
{
    public static class MongoDatabaseInitializer
    {
        private const string MongoUserName = "COMPLEXITY_MONGO_USERNAME";
        private const string MongoPassword = "COMPLEXITY_MONGO_PASSWORD";

        public static IMongoDatabase CreateMongoDatabase(
            string databaseName,
            string username = default,
            string password = default)
        {
            SetupDatabase();
            return Connect(databaseName, username, password/*,name: "Testing"*/);
        }

        private static IMongoDatabase Connect(string databaseName, string username = default, string password = default,string name = "QuizDatabase")
        {
            username = username ?? Environment.GetEnvironmentVariable(MongoUserName);
            password = password ?? Environment.GetEnvironmentVariable(MongoPassword);
            var connectionString = username is null || password is null
                ? "mongodb://localhost:27017"
                : $"mongodb://{username}:{password}@quizcluster-shard-00-00-kzjb8.azure.mongodb.net:27017," +
                  "quizcluster-shard-00-01-kzjb8.azure.mongodb.net:27017," +
                  "quizcluster-shard-00-02-kzjb8.azure.mongodb.net:27017/" +
                  $"{databaseName}?ssl=true&replicaSet=QuizCluster-shard-0&authSource=admin&retryWrites=true";
            var client = new MongoClient(connectionString);
            return client.GetDatabase(name);
        }

        internal static void SetupDatabase()
        {
            BsonClassMap.RegisterClassMap<Entity<Guid>>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id);
                cm.AddKnownType(typeof(Entity));
            });

            BsonClassMap.RegisterClassMap<Entity>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
            });

            MongoHelpers.AutoRegisterClassMap<Level>(c => new Level(c.Id, c.Description, c.Generators, c.NextLevels));
            MongoHelpers.AutoRegisterClassMap<Topic>(c => new Topic(c.Id, c.Name, c.Description, c.Levels));

            MongoHelpers.AutoRegisterClassMap<TemplateTaskGenerator>(c => new TemplateTaskGenerator(c.Id, c.PossibleAnswers,
                c.Text, c.Hints,
                c.Answer, c.Streak, c.Question));
            MongoHelpers.AutoRegisterClassMap<TaskGenerator>(cm => cm.SetIsRootClass(true));
            MongoHelpers.AutoRegisterClassMap<TaskInfoEntity>(c => new TaskInfoEntity(c.Question, c.Answer, c.Hints, c.HintsTaken,
                c.ParentGeneratorId, c.IsSolved, c.Id));

            MongoHelpers.AutoRegisterClassMap<UserEntity>(c => new UserEntity(c.Id, c.UserProgressEntity));

            MongoHelpers.AutoRegisterClassMap<LevelProgressEntity>(cm =>
            {
                MongoHelpers.MapDictionary<LevelProgressEntity, Guid, int>(cm, c => c.CurrentLevelStreaks);
                cm.MapCreator(c => new LevelProgressEntity(c.LevelId, c.CurrentLevelStreaks, c.Id));
            });
            MongoHelpers.AutoRegisterClassMap<TopicProgressEntity>(cm =>
            {
                MongoHelpers.MapDictionary<TopicProgressEntity, Guid, LevelProgressEntity>(cm, c => c.LevelProgressEntities);
                cm.MapCreator(c => new TopicProgressEntity(c.LevelProgressEntities, c.TopicId, c.Id));
            });
            MongoHelpers.AutoRegisterClassMap<UserProgressEntity>(cm =>
            {
                MongoHelpers.MapDictionary<UserProgressEntity, Guid, TopicProgressEntity>(cm, c => c.TopicsProgress);
                cm.MapCreator(c => new UserProgressEntity(c.CurrentTopicId, c.CurrentLevelId, c.UserId,
                    c.TopicsProgress, c.CurrentTask, c.Id));
            });
        }

       

    }
}