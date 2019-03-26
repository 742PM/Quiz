using System;

namespace DataBase.DatabaseEntities
{
    public class TaskInfoEntity
    {
        public string Question { get; set; }

        public string Answer { get; set; }

        public string[] Hints { get; set; }

        public int HintsTaken { get; set; }

        public Guid ParentGeneratorId { get; set; }

        public bool IsSolved { get; set; }
    }
}
