using MongoDB.Bson.Serialization.Attributes;

namespace QuizBotCore.States
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(AboutState), typeof(UnknownUserState), /* typeof(WelcomeState), */
        typeof(LevelSelectionState), typeof(TaskState), typeof(FeedBackState), typeof(TopicSelectionState))]
    public abstract class State
    {
    }
}
