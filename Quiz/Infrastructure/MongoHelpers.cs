using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure
{
    public static class MongoHelpers
    {
        public static BsonMemberMap MapDictionary<TClass, TKey, TValue>(
            this BsonClassMap<TClass> cm,
            Expression<Func<TClass, Dictionary<TKey, TValue>>> memberLambda) =>
            cm.MapMember(memberLambda)
                .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<TKey, TValue>
                >(DictionaryRepresentation.ArrayOfDocuments));

        public static void AutoRegisterClassMap<TClass>(Expression<Func<TClass, TClass>> creatorLambda) =>
            AutoRegisterClassMap<TClass>(cm => cm.MapCreator(creatorLambda));

        public static void AutoRegisterClassMap<T>(Action<BsonClassMap<T>> additionalAction = null)
        {
            BsonClassMap.RegisterClassMap<T>(cm =>
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