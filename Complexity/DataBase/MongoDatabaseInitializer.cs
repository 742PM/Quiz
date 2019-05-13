using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Application.Repositories;
using Application.Repositories.Entities;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using Infrastructure.DDD;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using static MongoDB.Bson.Serialization.BsonClassMap;

namespace DataBase
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
            return Connect(databaseName, username, password);
        }

        private static IMongoDatabase Connect(string databaseName, string username = default, string password = default)
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
            return client.GetDatabase("QuizDatabase");
        }

        internal static void SetupDatabase()
        {
            RegisterClassMap<Entity<Guid>>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id);
                cm.AddKnownType(typeof(Entity));
            });

            RegisterClassMap<Entity>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
            });

            AutoRegisterClassMap<Level>(c => new Level(c.Id, c.Description, c.Generators, c.NextLevels));
            AutoRegisterClassMap<Topic>(c => new Topic(c.Id, c.Name, c.Description, c.Levels));

            AutoRegisterClassMap<TemplateTaskGenerator>(c => new TemplateTaskGenerator(c.Id, c.PossibleAnswers,
                c.Text, c.Hints,
                c.Answer, c.Streak, c.Question));
            AutoRegisterClassMap<TaskGenerator>(cm => cm.SetIsRootClass(true));
            AutoRegisterClassMap<TaskInfoEntity>(c => new TaskInfoEntity(c.Question, c.Answer, c.Hints, c.HintsTaken,
                c.ParentGeneratorId, c.IsSolved, c.Id));

            AutoRegisterClassMap<UserEntity>(c => new UserEntity(c.Id, c.UserProgressEntity));
            AutoRegisterClassMap<AdminEntity>(c => new AdminEntity(c.Id, c.PasswordHash));

            AutoRegisterClassMap<LevelProgressEntity>(cm =>
            {
                cm.MapDictionary(c => c.CurrentLevelStreaks);
                cm.MapCreator(c => new LevelProgressEntity(c.LevelId, c.CurrentLevelStreaks, c.Id));
            });
            AutoRegisterClassMap<TopicProgressEntity>(cm =>
            {
                cm.MapDictionary(c => c.LevelProgressEntities);
                cm.MapCreator(c => new TopicProgressEntity(c.LevelProgressEntities, c.TopicId, c.Id));
            });
            AutoRegisterClassMap<UserProgressEntity>(cm =>
            {
                cm.MapDictionary(c => c.TopicsProgress);
                cm.MapCreator(c => new UserProgressEntity(c.CurrentTopicId, c.CurrentLevelId, c.UserId,
                    c.TopicsProgress, c.CurrentTask, c.Id));
            });
        }

        private static BsonMemberMap MapDictionary<TClass, TKey, TValue>(
            this BsonClassMap<TClass> cm,
            Expression<Func<TClass, Dictionary<TKey, TValue>>> memberLambda) =>
            cm.MapMember(memberLambda)
                .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<TKey, TValue>
                >(DictionaryRepresentation.ArrayOfDocuments));

        private static void AutoRegisterClassMap<TClass>(Expression<Func<TClass, TClass>> creatorLambda) =>
            AutoRegisterClassMap<TClass>(cm => cm.MapCreator(creatorLambda));

        private static void AutoRegisterClassMap<T>(Action<BsonClassMap<T>> additionalAction = null)
        {
            RegisterClassMap<T>(cm =>
            {
                var propertyInfos = typeof(T)
                    .GetProperties(BindingFlags.DeclaredOnly |
                                   BindingFlags.Public |
                                   BindingFlags.Instance)
                    .Cast<MemberInfo>()
                    .ToList();
                foreach (var propertyInfo in propertyInfos)
                    cm.MapMember(propertyInfo);
                additionalAction?.Invoke(cm);
            });
        }
    }
}