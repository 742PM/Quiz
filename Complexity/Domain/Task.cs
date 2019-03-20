using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    [Value]
    public struct Task : IEquatable<Task>
    {
        public Task(string[] answers, string question, string[] hints, string rightAnswer,Guid generatorId)
        {
            Answers = answers;
            Question = question;
            ParentGeneratorId = generatorId;
            Hints = hints;
            RightAnswer = rightAnswer;
        }
        
        public string Question { get; }

        public string[] Answers { get; }

        public string[] Hints { get; }

        public string RightAnswer { get; }

        public bool Equals(Task other) =>
            (Answers, Question, Hints, RightAnswer).Equals((other.Answers, other.Question, other.Hints,
                                                            other.RightAnswer));

        public void Deconstruct(out string question, out string[] answers, out string[] hints, out string rightAnswer)
        {
            question = Question;
            answers = Answers;
            hints = Hints;
            rightAnswer = RightAnswer;
        }

        public Guid ParentGeneratorId { get; }

        public override bool Equals(object obj) => obj is Task task && Equals(task);

        public override int GetHashCode() => (Answers, Question, Hints, RightAnswer).GetHashCode();

        public static bool operator ==(Task left, Task right) => Equals(left, right);

        public static bool operator !=(Task left, Task right) => !Equals(left, right);
    }
}
