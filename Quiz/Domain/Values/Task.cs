using System;
using System.Linq;
using Infrastructure;
using Infrastructure.DDD;
using Infrastructure.Result;

namespace Domain.Values
{
    [Value]
    [MustBeSaved]
    public struct Task : IEquatable<Task>
    {
        public Task(
            string text,
            string[] hints,
            string answer,
            Guid generatorId,
            string[] possibleAnswers,
            string question)
        {
            Text = text;
            ParentGeneratorId = generatorId;
            PossibleAnswers = possibleAnswers;
            Question = question;
            Hints = hints;
            Answer = answer;
        }

        public Task With(string answer) => new Task(Text, Hints, answer, ParentGeneratorId, PossibleAnswers, Question);

        public string Text { get; }

        public string[] PossibleAnswers { get; }
        public string Question { get; }

        public string[] Hints { get; }

        public string Answer { get; }

        public Task With(string[] answers) => new Task(Text, Hints, Answer, ParentGeneratorId, answers, Question);

        public void Deconstruct(
            out string question,
            out Maybe<string[]> hints,
            out string answer,
            out Maybe<string[]> possibleAnswers,
            out Guid generatorId,
            out string exactQuestion)
        {
            question = Text;
            hints = Hints;
            answer = Answer;
            possibleAnswers = PossibleAnswers;
            generatorId = ParentGeneratorId;
            exactQuestion = Question;
        }

        public Guid ParentGeneratorId { get; }

        public override bool Equals(object obj) => obj is Task task && Equals(task);

        public bool Equals(Task other) =>
            string.Equals(Text, other.Text) && PossibleAnswers.SequenceEqual(other.PossibleAnswers) &&
            string.Equals(Question, other.Question) && Hints.SequenceEqual(other.Hints) &&
            string.Equals(Answer, other.Answer) && ParentGeneratorId.Equals(other.ParentGeneratorId);

        public override int GetHashCode()
        {
            return (Text, Hints, Answer, ParentGeneratorId, Question, PossibleAnswers).GetHashCode();
        }

        public static bool operator ==(Task left, Task right) => Equals(left, right);

        public static bool operator !=(Task left, Task right) => !Equals(left, right);
    }
}