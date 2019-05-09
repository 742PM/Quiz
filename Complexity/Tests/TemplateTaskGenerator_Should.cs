using System;
using Domain.Entities.TaskGenerators;
using FluentAssertions;
using NUnit.Framework;
using static Infrastructure.Result.None;
namespace Tests
{
    [TestFixture]
    public class TemplateTaskGenerator_Should
    {

        private const string QuestionDummy = "question";
        private const string AnswerDummy = "answer";
        private const int StreakDummy = 3;
        private static readonly Guid IdDummy = Guid.Empty;
        private static readonly string[] ArrayDummy = new string[0];

        [Test]
        [Category("Creation")]
        public void Throw_WhenHintsOrPossibleAnswers_AreNull()
        {
            Action nullHintsCreation = () => new TemplateTaskGenerator(IdDummy, null, QuestionDummy, ArrayDummy, AnswerDummy, StreakDummy);
            Action nullAnswersCreation = () => new TemplateTaskGenerator(IdDummy, ArrayDummy, QuestionDummy, null, AnswerDummy, StreakDummy);
            nullAnswersCreation.Should().Throw<ArgumentException>("answers can not be null");
            nullHintsCreation.Should().Throw<ArgumentException>("hints can not be null");
        }

    }
    
}