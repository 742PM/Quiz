using System;

namespace Domain.Values
{
    [Value]
    [MustBeSaved]
    public struct Task : IEquatable<Task>
    {
        public Task(
            string question,
            string[] hints,
            string answer,
            Guid generatorId,
            string[] possibleAnswers)
        {
            Question = question;
            ParentGeneratorId = generatorId;
            PossibleAnswers = possibleAnswers;
            Hints = hints;
            Answer = answer;
        }

        public Task With(string answer) => new Task(Question, Hints, answer, ParentGeneratorId, PossibleAnswers);
        public string Question { get; }

        public string[] PossibleAnswers { get; }

        public string[] Hints { get; }

        public string Answer { get; }

        public bool Equals(Task other) =>
            (Question, Hints, RightAnswer: Answer).Equals((other.Question, other.Hints, other.Answer));

        public Task With(string[] answers) => new Task(Question, Hints, Answer, ParentGeneratorId, answers);

        public void Deconstruct(out string question, out string[] hints, out string rightAnswer)
        {
            question = Question;
            hints = Hints;
            rightAnswer = Answer;
        }

        public Guid ParentGeneratorId { get; }

        public override bool Equals(object obj) => obj is Task task && Equals(task);

        public override int GetHashCode() => (Question, Hints, RightAnswer: Answer).GetHashCode();

        public static bool operator ==(Task left, Task right) => Equals(left, right);

        public static bool operator !=(Task left, Task right) => !Equals(left, right);
    }
}
