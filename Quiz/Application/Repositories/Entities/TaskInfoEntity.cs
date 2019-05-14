using System;
using Infrastructure.DDD;

namespace Application.Repositories.Entities
{
    public class TaskInfoEntity : Entity
    {
        /// <inheritdoc />
        public override string ToString() => $"{base.ToString()}, {nameof(Question)}: {Question}, {nameof(Answer)}: {Answer}, {nameof(Hints)}: {Hints}, {nameof(HintsTaken)}: {HintsTaken}, {nameof(ParentGeneratorId)}: {ParentGeneratorId}, {nameof(IsSolved)}: {IsSolved}";

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
        public string Question { get; }

        public string Answer { get; }

        public string[] Hints { get;  }

        public int HintsTaken { get;  }

        public Guid ParentGeneratorId { get;  }

        public bool IsSolved { get;  }
    }
}
