using System;

namespace DataBase
{
    public class Answer
    {
        public Guid AnswerId { get; set; }
        
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
    }
}