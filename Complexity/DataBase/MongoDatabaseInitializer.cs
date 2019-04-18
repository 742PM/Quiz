using System;
using System.Collections.Generic;
using DataBase.Entities;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using static MongoDB.Bson.Serialization.BsonClassMap;

namespace DataBase
{
    public static class MongoDatabaseInitializer
    {
        public static void SetupDatabase()
        {
            RegisterClassMap<Level>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(c => new Level(c.Id,c.Description,c.Generators,c.NextLevels));
            });
            RegisterClassMap<Topic>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(c => new Topic(c.Id,c.Description,c.Description,c.Levels));
            });

            RegisterClassMap<TemplateTaskGenerator>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(c => new TemplateTaskGenerator(c.Id, c.PossibleAnswers, c.TemplateCode, c.Hints, c.Answer,
                                                             c.Streak));
            });
            RegisterClassMap<TaskGenerator>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
            });

            RegisterClassMap<TaskInfoEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(c => new TaskInfoEntity(c.Question,c.Answer,c.Hints,c.HintsTaken,c.ParentGeneratorId,c.IsSolved));
            });
            RegisterClassMap<UserEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(c => new UserEntity(c.Id,c.UserProgressEntity));
            });

            RegisterClassMap<LevelProgressEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(c => new LevelProgressEntity(c.LevelId, c.CurrentLevelStreaks));
                cm.MapMember(c => c.CurrentLevelStreaks)
                  .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, int>
                                 >(DictionaryRepresentation.ArrayOfDocuments));
            });
            RegisterClassMap<TopicProgressEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(c => new TopicProgressEntity(c.LevelProgressEntities, c.TopicId));
                cm.MapMember(c => c.LevelProgressEntities)
                  .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, LevelProgressEntity>
                                 >(DictionaryRepresentation.ArrayOfDocuments));
            });
            RegisterClassMap<UserProgressEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(c => new UserProgressEntity(c.CurrentTopicId, c.CurrentLevelId, c.UserId,
                                                          c.TopicsProgress, c.CurrentTask));
                cm.MapMember(c => c.TopicsProgress)
                  .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, TopicProgressEntity>
                                 >(DictionaryRepresentation.ArrayOfDocuments));
            });
        }
    }
}
