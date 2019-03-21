using System;

namespace DataBase
{
    public class TaskEntity
    {
        public Guid TaskId { get; set; }
        public Guid GeneratorId { get; set; }

        public int Difficulty { get; set; }

        public string RightAnswer { get; set; }
        public string[] Answers { get; set; }

        public string[] Hints { get; set; }
        public string Question { get; set; }
    }
}