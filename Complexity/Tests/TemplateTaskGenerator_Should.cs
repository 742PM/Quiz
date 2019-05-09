using System;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Entities.TaskGenerators;
using Domain.Values;
using FluentAssertions;
using Infrastructure;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TemplateTaskGenerator_Should
    {
        private const string CodeDummy = "code";
        private const string QuestionDummy = "code";
        private const string AnswerDummy = "answer";
        private const int StreakDummy = 3;
        private const string RegexDummy = ".*";
        private static readonly Guid IdDummy = Guid.Empty;
        private static readonly string[] ArrayDummy = new string[0];

        private static void TestField(
            (string actual, string expected)? code = default,
            (string actual, string expected)? question = default,
            (string actual, string expected)? answer = default,
            (string[] actual, string[] expected)? answers = default,
            (string[] actual, string[] expected)? hints = default)
        {
            var (actualHints, expectedHints) = hints ?? (ArrayDummy, ArrayDummy);
            var (actualAnswers, expectedAnswers) = answers ?? (ArrayDummy, ArrayDummy);
            var (actualCode, expectedCode) = code ?? (CodeDummy, CodeDummy);
            var (actualAnswer, expectedAnswer) = answer ?? (AnswerDummy, AnswerDummy);
            var (actualQuestion, expectedQuestion) = question ?? (QuestionDummy, QuestionDummy);
            var expectation = new Task(expectedCode, expectedHints, expectedAnswer, IdDummy, expectedAnswers,
                                       expectedQuestion);
            var actual = new TemplateTaskGenerator(IdDummy, actualAnswers, actualCode, actualHints, actualAnswer,
                                                   StreakDummy,
                                                   actualQuestion)
                .GetTask(new Random());
            actual.Should().BeEquivalentTo(expectation);
        }

        private static void TestFieldWithRegex(
            (string actual, string regex)? code = default,
            (string actual, string regex)? question = default,
            (string actual, string regex)? answer = default,
            (string[] actual, string[] regexes)? answers = default,
            (string[] actual, string[] regexes)? hints = default)
        {
            var (actualHints, expectedHints) = hints ?? (ArrayDummy, ArrayDummy);
            var (actualAnswers, expectedAnswers) = answers ?? (ArrayDummy, ArrayDummy);
            var (actualCode, expectedCode) = code ?? (CodeDummy, RegexDummy);
            var (actualAnswer, expectedAnswer) = answer ?? (AnswerDummy, RegexDummy);
            var (actualQuestion, expectedQuestion) = question ?? (QuestionDummy, RegexDummy);

            var actual = new TemplateTaskGenerator(IdDummy, actualAnswers, actualCode, actualHints, actualAnswer,
                                                   StreakDummy,
                                                   actualQuestion)
                .GetTask(new Random());
            Regex.IsMatch(actual.Answer, expectedAnswer).Should().BeTrue();
            Regex.IsMatch(actual.Code, expectedCode).Should().BeTrue();
            Regex.IsMatch(actual.Question, expectedQuestion).Should().BeTrue();
            actual.PossibleAnswers.Zip(expectedAnswers, Regex.IsMatch).Should().AllBeEquivalentTo(true);
            actual.Hints.Zip(expectedHints, Regex.IsMatch).Should().AllBeEquivalentTo(true);
        }

        [TestCase("{{4+4}}", "8")]
        public void RenderTemplate_InAnswer(string actual, string expected) => TestField(answer: (actual, expected));

        [TestCase("{{4+4}}", "8")]
        public void RenderTemplate_InCode(string actual, string expected) => TestField((actual, expected));

        [TestCase("{{4+4}}", "8")]
        public void RenderTemplate_InQuestion(string actual, string expected) =>
            TestField(question: (actual, expected));

        [Test]
        public void CreateTemplatelessTasks() => TestField();

        [Test]
        public void RenderTemplate_InAnswers() => TestField(answers: (new[] { "{{4+4}}" }, new[] { "8" }));

        [Test]
        public void RenderTemplate_InHints() => TestField(hints: (new[] { "{{4+4}}" }, new[] { "8" }));

        [Test]
        public void RenderTemplates_WithSharedState() => TestField(("{{var = 4; var}}", "4"),
                                                                   answer: ("{{var}}", "4"), question: ("{{var}}", "4"),
                                                                   hints: (new[] { "{{var}}" }, new[] { "4" }),
                                                                   answers: (new[] { "{{var}}" }, new[] { "4" }));

        [TestCase("{{any_of ['m','n','k']}}", "(n|m|k)", TestName = nameof(TemplateLanguage.AnyOf))]
        [TestCase("{{random 100 200}}", @"\d{3}", TestName = nameof(TemplateLanguage.Random))]
        public void Render_WhenBuiltInFunctionsAreUsed(string actual, string regex) => TestFieldWithRegex(code: (actual, regex));

        [Test]
        public void Throw_WhenHintsOrPossibleAnswers_AreNull()
        {
            Action nullHintsCreation = () =>
                new TemplateTaskGenerator(IdDummy, null, CodeDummy, ArrayDummy, AnswerDummy, StreakDummy,
                                          QuestionDummy);
            Action nullAnswersCreation = () =>
                new TemplateTaskGenerator(IdDummy, ArrayDummy, CodeDummy, null, AnswerDummy, StreakDummy,
                                          QuestionDummy);
            nullAnswersCreation.Should().Throw<ArgumentException>("answers can not be null");
            nullHintsCreation.Should().Throw<ArgumentException>("hints can not be null");
        }
    }
}