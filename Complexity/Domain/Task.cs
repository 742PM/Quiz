using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    [Value]
    [MustBeSaved]
    public struct Task : IEquatable<Task>
    {
        public Task(string question, string[] hints, string rightAnswer,Guid generatorId)
        {
            Question = question;
            ParentGeneratorId = generatorId;
            Hints = hints;
            RightAnswer = rightAnswer;
        }
        public Task With(string answer) => new Task(Question,Hints,answer,ParentGeneratorId);
        public string Question { get; }


        public string[] Hints { get; }

        public string RightAnswer { get; }

        public bool Equals(Task other) =>
            ( Question, Hints, RightAnswer).Equals(( other.Question, other.Hints,
                                                            other.RightAnswer));

        public void Deconstruct(out string question,  out string[] hints, out string rightAnswer)
        {
            question = Question;
            hints = Hints;
            rightAnswer = RightAnswer;
        }

        public Guid ParentGeneratorId { get; }

        public override bool Equals(object obj) => obj is Task task && Equals(task);

        public override int GetHashCode() => ( Question, Hints, RightAnswer).GetHashCode();

        public static bool operator ==(Task left, Task right) => Equals(left, right);

        public static bool operator !=(Task left, Task right) => !Equals(left, right);
    }
}
