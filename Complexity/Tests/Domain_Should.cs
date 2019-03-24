using System;
using Domain;
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
            new[]
                {
                    new Topic(Guid.Empty, "Some Topic", "Some description",
                              new[]
                              {
                                  new Level(Guid.Empty, "Yet another description",
                                            new (TaskGenerator, int)[]
                                            {
                                                (new ExampleTaskGenerator("Woah!", Guid.Empty), 3),
                                                (new TemplateTaskGenerator(Guid.Empty, new string[0], "Woah! That is some real code!", new string[0], "Woah! This is an ANSWER!"),
                                                 2),
                                                (new ExampleTaskGenerator("Woah!", Guid.Empty), 1)
                                            })
                              })
                }.Should()
                 .NotContainNulls() 
//               .And.BeFine();
;
        }
    }
}
