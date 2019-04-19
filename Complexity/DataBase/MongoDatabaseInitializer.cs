using System;
using System.Collections.Generic;
using Application.Repositories.Entities;
using Domain.Entities;
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
        public static IMongoDatabase Connect(string databaseName, string username = default, string password = default)
        {
            username = username ?? Environment.GetEnvironmentVariable("COMPLEXITY_MONGO_USERNAME");
            password = password ?? Environment.GetEnvironmentVariable("COMPLEXITY_MONGO_PASSWORD");
            var client = new MongoClient
            ($"mongodb://{username}:{password}@quizcluster-shard-00-00-kzjb8.azure.mongodb.net:27017," +
             "quizcluster-shard-00-01-kzjb8.azure.mongodb.net:27017," +
             "quizcluster-shard-00-02-kzjb8.azure.mongodb.net:27017/" +
             $"{databaseName}?ssl=true&replicaSet=QuizCluster-shard-0&authSource=admin&retryWrites=true");
            return client.GetDatabase("test");
        }

        public static void SetupDatabase()
        {
            RegisterClassMap<Entity<Guid>>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.Id);
                cm.AddKnownType(typeof(Entity));
            });

            RegisterClassMap<Entity>(cm =>
            {
                cm.AutoMap();
                cm.AddKnownType(typeof(LevelProgressEntity));
                cm.AddKnownType(typeof(TaskInfoEntity));
                cm.AddKnownType(typeof(TopicProgressEntity));
                cm.AddKnownType(typeof(UserEntity));
                cm.AddKnownType(typeof(UserProgressEntity));
                cm.AddKnownType(typeof(TaskGenerator));
                cm.AddKnownType(typeof(Level));
                cm.AddKnownType(typeof(Topic));
                cm.SetIsRootClass(true);
            });

            RegisterClassMap<Level>(cm =>
            {
                cm.MapMember(c => c.Description);
                cm.MapMember(c => c.Generators);
                cm.MapMember(c => c.NextLevels);
                cm.MapCreator(c => new Level(c.Id, c.Description, c.Generators, c.NextLevels));
            });
            RegisterClassMap<Topic>(cm =>
            {
                cm.MapMember(c => c.Description);
                cm.MapMember(c => c.Levels);
                cm.MapMember(c => c.Name);

                cm.MapCreator(c => new Topic(c.Id, c.Name, c.Description, c.Levels));
            });

            RegisterClassMap<TemplateTaskGenerator>(cm =>
            {
                cm.MapMember(c => c.Answer);
                cm.MapMember(c => c.Hints);
                cm.MapMember(c => c.PossibleAnswers);
                cm.MapMember(c => c.TemplateCode);

                cm.MapCreator(c => new TemplateTaskGenerator(c.Id, c.PossibleAnswers, c.TemplateCode, c.Hints, c.Answer,
                                                             c.Streak));
            });
            RegisterClassMap<TaskGenerator>(cm =>
            {
                cm.MapMember(c => c.Streak);
                cm.SetIsRootClass(true);
            });

            RegisterClassMap<TaskInfoEntity>(cm =>
            {
                cm.MapMember(c => c.Answer);
                cm.MapMember(c => c.Hints);
                cm.MapMember(c => c.HintsTaken);
                cm.MapMember(c => c.IsSolved);
                cm.MapMember(c => c.ParentGeneratorId);
                cm.MapMember(c => c.Question);
                cm.MapCreator(c => new TaskInfoEntity(c.Question, c.Answer, c.Hints, c.HintsTaken, c.ParentGeneratorId, c.IsSolved, c.Id));
            });
            RegisterClassMap<UserEntity>(cm =>
            {
                cm.MapMember(c => c.UserProgressEntity);
                cm.MapCreator(c => new UserEntity(c.Id, c.UserProgressEntity));
            });

            RegisterClassMap<LevelProgressEntity>(cm =>
            {
                cm.MapMember(c => c.LevelId);
                cm.MapMember(c => c.CurrentLevelStreaks)
                  .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, int>
                                 >(DictionaryRepresentation.ArrayOfDocuments));
                cm.MapCreator(c => new LevelProgressEntity(c.LevelId, c.CurrentLevelStreaks, c.Id));

            });
            RegisterClassMap<TopicProgressEntity>(cm =>
            {
                cm.MapMember(c => c.TopicId);
                cm.MapMember(c => c.LevelProgressEntities)
                  .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, LevelProgressEntity>
                                 >(DictionaryRepresentation.ArrayOfDocuments));
                cm.MapCreator(c => new TopicProgressEntity(c.LevelProgressEntities, c.TopicId, c.Id));
            });
            RegisterClassMap<UserProgressEntity>(cm =>
            {
                cm.MapMember(c => c.CurrentLevelId);
                cm.MapMember(c => c.CurrentTask);
                cm.MapMember(c => c.CurrentTopicId);
                cm.MapMember(c => c.UserId);
                cm.MapMember(c => c.TopicsProgress)
                  .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, TopicProgressEntity>
                                 >(DictionaryRepresentation.ArrayOfDocuments));
                cm.MapCreator(c => new UserProgressEntity(c.CurrentTopicId, c.CurrentLevelId, c.UserId,
                                                          c.TopicsProgress, c.CurrentTask, c.Id));
            });
        }
    }
}