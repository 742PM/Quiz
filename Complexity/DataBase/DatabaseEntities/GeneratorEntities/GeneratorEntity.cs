using DataBase.DatabaseEntities.GeneratorEntities;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBase.DatabaseEntities
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(TemplateGeneratorEntity))]
    public abstract class GeneratorEntity
    {
        string Description { get; set; }
        int Streak { get; set; }
    }
}