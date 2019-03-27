using System;
using System.Collections.Generic;
using Domain.Entities.TaskGenerators;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace DataBase.Entities
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var mongoConnectionString = Environment.GetEnvironmentVariable("COMPLEXITY_MONGO_CONNECTION_STRING") ??
                                        "mongodb://localhost:27017";
            var db = new MongoClient(mongoConnectionString).GetDatabase("ComplexityBot");
            var userRepo = new MongoUserRepository(db);

            BsonClassMap.RegisterClassMap<TaskGenerator>(tg =>
            {
                tg.AutoMap();
                tg.SetIsRootClass(true);
            });

            BsonClassMap.RegisterClassMap<TemplateTaskGenerator>();
            BsonClassMap.RegisterClassMap<ExampleTaskGenerator>();

            BsonClassMap.RegisterClassMap<LevelProgressEntity>(lpe =>
            {
                lpe.AutoMap();
                lpe.MapMember(c => c.CurrentLevelStreaks).SetSerializer(
                    new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, int>>(DictionaryRepresentation
                        .ArrayOfDocuments));
            });

            BsonClassMap.RegisterClassMap<TopicProgressEntity>(tpe =>
            {
                tpe.AutoMap();
                tpe.MapMember(c => c.LevelProgressEntities).SetSerializer(
                    new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, LevelProgressEntity>>(
                        DictionaryRepresentation.ArrayOfDocuments));
            });

            BsonClassMap.RegisterClassMap<UserProgressEntity>(upe =>
            {
                upe.AutoMap();
                upe.MapMember(c => c.TopicsProgress).SetSerializer(
                    new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, TopicProgressEntity>>(
                        DictionaryRepresentation.ArrayOfDocuments));
            });

            var levelProgressEntity = new LevelProgressEntity
            {
                LevelId = Guid.NewGuid(),
                CurrentLevelStreaks = new Dictionary<Guid, int>
                {
                    {
                        Guid.NewGuid(), 10
                    }
                }
            };

            var topicProgressEntity = new TopicProgressEntity
            {
                TopicId = Guid.NewGuid(),
                LevelProgressEntities = new Dictionary<Guid, LevelProgressEntity>
                {
                    {
                        Guid.NewGuid(), levelProgressEntity
                    }
                }
            };
            
            var userProgress = new UserProgressEntity
            {
                UserId = Guid.NewGuid(),
                CurrentLevelId = Guid.NewGuid(),
                CurrentTopicId = Guid.NewGuid(),
                TopicsProgress = new Dictionary<Guid, TopicProgressEntity>
                {
                    {
                        Guid.NewGuid(), topicProgressEntity
                    }
                }
            };
            
            var user = new UserEntity(Guid.NewGuid(), userProgress);

            userRepo.Insert(user);
        }
    }
}