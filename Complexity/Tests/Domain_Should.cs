using System;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Domain_Should
    {
        [Test]
        public void AcceptItself()
        {
            const string whoa = "Whoa";
            new[]
                {
                    new Topic(Guid.Empty, "Some Topic", "Some description",
                              new[]
                              {
                                  new Level(Guid.Empty, "Yet another description",
                                            new TaskGenerator[]
                                            {
                                                new ExampleTaskGenerator($"{whoa}!", Guid.Empty),
                                                new TemplateTaskGenerator(Guid.Empty, new string[0],
                                                                          $"{whoa}! That is some real code!",
                                                                          new string[0],
                                                                          $"{whoa}! This is an ANSWER!", 2),
                                                new ExampleTaskGenerator($"{whoa}!", Guid.Empty)
                                            }, new Guid[0])
                              })
                }.Should()
                 .NotContainNulls()
//               .And.BeFine();
                ;
        }
    }
}
