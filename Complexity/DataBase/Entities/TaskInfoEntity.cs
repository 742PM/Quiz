using System;

namespace DataBase.Entities
{
    public class TaskInfoEntity
    {
        public TaskInfoEntity(string question, string answer, string[] hints, int hintsTaken, Guid parentGeneratorId, bool isSolved)
        {
            Question = question;
            Answer = answer;
            Hints = hints;
            HintsTaken = hintsTaken;
            ParentGeneratorId = parentGeneratorId;
            IsSolved = isSolved;
        }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string[] Hints { get; set; }

        public int HintsTaken { get; set; }

        public Guid ParentGeneratorId { get; set; }

        public bool IsSolved { get; set; }
    }
}
