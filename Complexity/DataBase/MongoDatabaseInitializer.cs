using System;
using System.Collections.Generic;
using DataBase.Entities;
using Domain.Entities.TaskGenerators;
using Infrastructure.DDD;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using static MongoDB.Bson.Serialization.BsonClassMap;

namespace DataBase
{
    public static class MongoDatabaseInitializer
    {
        public static IMongoDatabase Connect()
        {
            var username = Environment.GetEnvironmentVariable("COMPLEXITY_MONGO_USERNAME");
            var password = Environment.GetEnvironmentVariable("COMPLEXITY_MONGO_PASSWORD");
            var client = new MongoClient
            ($"mongodb://{username}:{password}@quizcluster-shard-00-00-kzjb8.azure.mongodb.net:27017," +
             "quizcluster-shard-00-01-kzjb8.azure.mongodb.net:27017," +
             "quizcluster-shard-00-02-kzjb8.azure.mongodb.net:27017/" +
             "test?ssl=true&replicaSet=QuizCluster-shard-0&authSource=admin&retryWrites=true");
            return client.GetDatabase("test");
        }

        public static void SetupDatabase()
        {
            RegisterClassMap<Entity<Guid>>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
            });
            RegisterClassMap<TemplateTaskGenerator>();
            RegisterClassMap<ExampleTaskGenerator>();
            RegisterClassMap<TaskGenerator>();

            RegisterClassMap<LevelProgressEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.CurrentLevelStreaks)
                    .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, int>
                    >(DictionaryRepresentation.ArrayOfDocuments));
            });

            RegisterClassMap<TopicProgressEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.LevelProgressEntities)
                    .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, LevelProgressEntity>
                    >(DictionaryRepresentation.ArrayOfDocuments));
            });

            RegisterClassMap<UserProgressEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.TopicsProgress)
                    .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, TopicProgressEntity>
                    >(DictionaryRepresentation.ArrayOfDocuments));
            });
        }
    }
}