using System;
using System.Diagnostics.CodeAnalysis;

namespace DataBase.Entities
{
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    public class TaskInfoEntity
    {
        public TaskInfoEntity(
            string question,
            string answer,
            string[] hints,
            int hintsTaken,
            Guid parentGeneratorId,
            bool isSolved)
        {
            Question = question;
            Answer = answer;
            Hints = hints;
            HintsTaken = hintsTaken;
            ParentGeneratorId = parentGeneratorId;
            IsSolved = isSolved;
        }

        public string Question { get; private set; }

        public string Answer { get; private set; }

        public string[] Hints { get; private set; }

        public int HintsTaken { get; private set; }

        public Guid ParentGeneratorId { get; private set; }

        public bool IsSolved { get; private set; }

        public TaskInfoEntity With(
            string question = default,
            string answer = default,
            string[] hints = default,
            int? hintsTaken = default,
            Guid? parentGeneratorId = default,
            bool? isSolved = default) =>
            new TaskInfoEntity(question ?? Question, answer ?? Answer, hints ?? Hints, hintsTaken ?? HintsTaken,
                               parentGeneratorId ?? ParentGeneratorId, isSolved ?? IsSolved);
    }
}
