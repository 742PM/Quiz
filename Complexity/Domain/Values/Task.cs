using System;
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
            string question,
            string[] hints,
            string answer,
            Guid generatorId,
            string[] possibleAnswers,
            string exactQuestion)
        {
            Question = question;
            ParentGeneratorId = generatorId;
            PossibleAnswers = possibleAnswers;
            ExactQuestion = exactQuestion;
            Hints = hints;
            Answer = answer;
        }

        public Task With(string answer)
        {
            return new Task(Question, Hints, answer, ParentGeneratorId, PossibleAnswers, ExactQuestion);
        }

        public string Question { get; }

        public string[] PossibleAnswers { get; }
        public string ExactQuestion { get; }

        public string[] Hints { get; }

        public string Answer { get; }

        public bool Equals(Task other)
        {
            return (Question, Hints, Answer).Equals((other.Question, other.Hints, other.Answer));
        }

        public Task With(string[] answers)
        {
            return new Task(Question, Hints, Answer, ParentGeneratorId, answers, ExactQuestion);
        }

        public void Deconstruct(
            out string question,
            out Maybe<string[]> hints,
            out string answer,
            out Maybe<string[]> possibleAnswers)
        {
            question = Question;
            hints = Hints;
            answer = Answer;
            possibleAnswers = PossibleAnswers;
        }

        public Guid ParentGeneratorId { get; }

        public override bool Equals(object obj)
        {
            return obj is Task task && Equals(task);
        }

        public override int GetHashCode()
        {
            return (Question, Hints, Answer).GetHashCode();
        }

        public static bool operator ==(Task left, Task right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Task left, Task right)
        {
            return !Equals(left, right);
        }
    }
}