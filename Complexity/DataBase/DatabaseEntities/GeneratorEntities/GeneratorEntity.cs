using MongoDB.Bson.Serialization.Attributes;

namespace DataBase.DatabaseEntities.GeneratorEntities
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(TemplateGeneratorEntity))]
    public abstract class GeneratorEntity
    {
        private string Description { get; set; }
        private int Streak { get; set; }
    }
}
