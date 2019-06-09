using System;
using MongoDB.Bson.Serialization.Attributes;
using QuizBotCore.States;
using QuizRequestService.DTO;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace QuizBotCore.Database
{
    public class UserEntity 
    {
        [BsonConstructor]
        public UserEntity(State currentState, long telegramId, Guid id, int messageId, TaskDTO currentTask = null)
        {
            CurrentState = currentState;
            CurrentTask = currentTask;
            TelegramId = telegramId;
            MessageId = messageId;
            Id = id;
        }
        [BsonId]
        public Guid Id { get; private set; }
        
        [BsonElement]
        public int MessageId { get; private set; }
        
        [BsonElement]
        public State CurrentState { get; private set; }
        
        [BsonElement]
        public TaskDTO CurrentTask { get; private set; }

        [BsonElement]
        public long TelegramId { get; private set; }
    }
    

}
