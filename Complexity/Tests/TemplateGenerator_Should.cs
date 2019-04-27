using System;
using Domain.Entities.TaskGenerators;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using static Domain.Entities.TaskGenerators.TemplateTaskGenerator;

namespace Tests
{
    [TestFixture]
    public class TemplateGenerator_Should
    {
        [SetUp]
        public void SetUp()
        {
            random = A.Fake<Random>();
            A.CallTo(() => random.Next(0, 25)).Returns(0);
        }

        private Random random;

        private static TemplateTaskGenerator CreateGenerator(string template) =>
            new TemplateTaskGenerator(Guid.Empty, Array.Empty<string>(), template, Array.Empty<string>(), "", 10);

        [TestCase("var {{loop_var}} = 3;", "var a = 3;", TestName = "template has one variable")]
        [TestCase("var {{loop_var}} = 3;\n {{loop_var}}++;", "var a = 3;\n a++;", TestName =
            "template has two variable places")]
        [TestCase("var {{loop_var}} = 3;\n var {{loop_var1}} = 4;", "var a = 3;\n var b = 4;", TestName =
            "template has several variables")]
        public void ReplaceVariable_When(string template, string expected)
        {
            A.CallTo(() => random.Next(0, 25)).ReturnsNextFromSequence(0, 1, 2, 3, 4, 5, 6, 7);
            CreateGenerator(template).GetTask(random).Question.Should().BeEquivalentTo(expected);
        }

        [TestCase("var a = {{const}};\n var b = {{const1}};", "var a = 0;\n var b = 0;", TestName =
            "template has several constants")]
        [TestCase("var a = {{const}};", "var a = 0;", TestName = "template has one constants")]
        public void ReplaceConstants_When(string template, string expected)
        {
            A.CallTo(() => random.Next(-MaxRandomConstantValue, MaxRandomConstantValue))
             .Returns(0);
            CreateGenerator(template).GetTask(random).Question.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void BeOk_WhenNoTemplate()
        {
            Action creation = () => CreateGenerator("5 == 5;");
            creation.Should().NotThrow("Generator contains template processing;");
        }



        [Test]
        public void BeOk_WhenTemplateWithoutVariables()
        {
            CreateGenerator("5 == {{5}};").GetTask(random).Question.Should().BeEquivalentTo("5 == 5;");
        }
    }
}
