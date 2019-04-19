using System;
using Infrastructure.DDD;

namespace Application.Repositories.Entities
{
    public class TaskInfoEntity : Entity
    {
        public TaskInfoEntity(string question, string answer, string[] hints, int hintsTaken, Guid parentGeneratorId, bool isSolved, Guid id): base(id)
        {
            Question = question;
            Answer = answer;
            Hints = hints;
            HintsTaken = hintsTaken;
            ParentGeneratorId = parentGeneratorId;
            IsSolved = isSolved;
        }
        public TaskInfoEntity With(
            string question = default,
            string answer = default,
            string[] hints = default,
            int? hintsTaken = default,
            Guid? parentGeneratorId = default,
            bool? isSolved = default,
            Guid? id = default) =>
            new TaskInfoEntity(question ?? Question, answer ?? Answer, hints ?? Hints, hintsTaken ?? HintsTaken,
                               parentGeneratorId ?? ParentGeneratorId, isSolved ?? IsSolved, id ?? Id);
        public string Question { get; set; }

        public string Answer { get; set; }

        public string[] Hints { get; set; }

        public int HintsTaken { get; set; }

        public Guid ParentGeneratorId { get; set; }

        public bool IsSolved { get; set; }
    }
}
