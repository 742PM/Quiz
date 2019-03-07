using System;

namespace DataBase
{
    public class AnswerDTO
    {
        public Guid AnswerId { get; set; }
        
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
    }
}