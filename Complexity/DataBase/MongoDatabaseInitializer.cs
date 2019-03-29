using System;
using System.Collections.Generic;
using DataBase.Entities;
using Domain;
using Domain.Entities.TaskGenerators;
using Infrastructure;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using static MongoDB.Bson.Serialization.BsonClassMap;

namespace DataBase
{
    public static class MongoDatabaseInitializer
    {
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
