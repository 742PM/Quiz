using System;
using MongoDB.Driver;

namespace QuizBotCore.Database
{
    public class MessageTextRepository
    {
        public const string CollectionName = "telegramMessages";

        private readonly IMongoCollection<DialogMessages> textCollection;

        public DialogMessages Messages { get; private set; }

        public MessageTextRepository(IMongoDatabase database)
        {
            textCollection = database.GetCollection<DialogMessages>(CollectionName);
            var result = textCollection.Find(_ => true);
            if (result.Any())
            {
                Messages = result.First();
            }
            else
            {
                Messages = new DialogMessages(Guid.Empty);
                textCollection.InsertOne(Messages);
            }

        }

        public bool SetMessages(Guid id)
        {
            var result =  textCollection.Find(m => m.Id == id);
            if (result.Any())
            {
                Messages = result.First();
                return true;
            }

            return false;
        }

        public bool UpdateMessages()
            => SetMessages(Messages.Id);
    }
}