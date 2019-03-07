using System;

namespace DataBase
{
    public class AnswerEntity
    {
        public Guid AnswerId { get; set; }
        
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
    }
}