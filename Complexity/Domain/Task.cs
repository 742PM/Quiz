using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    [Value]
    public class Task : IEquatable<Task>
    {
        public Task(IEnumerable<string> answers, string question, IEnumerable<string> hints, string rightAnswer)
        {
            Answers = answers?.ToArray() ?? Array.Empty<string>();
            Question = question;
            Hints = hints?.ToArray() ?? Array.Empty<string>();
            RightAnswer = rightAnswer;
        }

        public string Question { get; }

        public string[] Answers { get; }

        public string[] Hints { get; }

        public string RightAnswer { get; }

        public bool Equals(Task other) =>
            other != null &&
            (Answers, Question, Hints, RightAnswer).Equals((other.Answers, other.Question, other.Hints,
                                                            other.RightAnswer));

        public void Deconstruct(out string question, out string[] answers, out string[] hints, out string rightAnswer)
        {
            question = Question;
            answers = Answers;
            hints = Hints;
            rightAnswer = RightAnswer;
        }

        public bool IsRightAnswer(string answer) => answer.Equals(Answers);

        public override bool Equals(object obj) => obj is Task task && Equals(task);

        public override int GetHashCode() => (Answers, Question, Hints, RightAnswer).GetHashCode();

        public static bool operator ==(Task left, Task right) => Equals(left, right);

        public static bool operator !=(Task left, Task right) => !Equals(left, right);
    }
}
