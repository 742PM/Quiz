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
            string code,
            string[] hints,
            string answer,
            Guid generatorId,
            string[] possibleAnswers,
            string question)
        {
            Code = code;
            ParentGeneratorId = generatorId;
            PossibleAnswers = possibleAnswers;
            Question = question;
            Hints = hints;
            Answer = answer;
        }

        public Task With(string answer)
        {
            return new Task(Code, Hints, answer, ParentGeneratorId, PossibleAnswers, Question);
        }

        public string Code { get; }

        public string[] PossibleAnswers { get; }
        public string Question { get; }

        public string[] Hints { get; }

        public string Answer { get; }

//        public bool Equals(Task other)
//        {
//            return (Code, Hints, Answer, Question, ParentGeneratorId).Equals((other.Code, other.Hints, other.Answer, other.Question, other.ParentGeneratorId));
//        }

        public Task With(string[] answers)
        {
            return new Task(Code, Hints, Answer, ParentGeneratorId, answers, Question);
        }

        public void Deconstruct(
            out string question,
            out Maybe<string[]> hints,
            out string answer,
            out Maybe<string[]> possibleAnswers, out Guid generatorId, out string exactQuestion)
        {
            question = Code;
            hints = Hints;
            answer = Answer;
            possibleAnswers = PossibleAnswers;
            generatorId = ParentGeneratorId;
            exactQuestion = Question;
        }

        public Guid ParentGeneratorId { get; }

        public override bool Equals(object obj)
        {
            return obj is Task task && Equals(task);
        }

        public bool Equals(Task other)
        {
            return string.Equals(Code, other.Code) && PossibleAnswers.SequenceEqual(other.PossibleAnswers) &&
                   string.Equals(Question, other.Question) && Hints.SequenceEqual(other.Hints) &&
                   string.Equals(Answer, other.Answer) && ParentGeneratorId.Equals(other.ParentGeneratorId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Code != null ? Code.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PossibleAnswers != null ? PossibleAnswers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Question != null ? Question.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Hints != null ? Hints.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Answer != null ? Answer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ParentGeneratorId.GetHashCode();
                return hashCode;
            }
        }
//        public override int GetHashCode()
//        {
//            return (Code, Hints, Answer, ParentGeneratorId, Question,PossibleAnswers).GetHashCode();
//        }

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