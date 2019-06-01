using System;
using System.Linq;
using Application.DTO;
using Application.Extensions;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ToDTO_Should
    {
        [Test]
        public void CastEmptyLevels()
        {
            new Topic(Guid.NewGuid(), "t", "opic", new Level[0])
                .ToDTO()
                .Should()
                .BeEquivalentTo(new TopicDto { Name = "t", Description = "opic", Levels = new LevelDto[0] });
        }

        [Test]
        public void CastNextLevels_ToInt()
        {
            var ids = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var levels = new[]
                         {
                             new Level(ids[0], "a", new TaskGenerator[0], new[] { ids[1] }),
                             new Level(ids[1], "b", new TaskGenerator[0], new[] { ids[2], ids[3] }),
                             new Level(ids[2], "c", new TaskGenerator[0], new Guid[0]),
                             new Level(ids[3], "d", new TaskGenerator[0], new Guid[0])
                         };
            new Topic(Guid.NewGuid(), "t", "opic", levels)
                .ToDTO()
                .Levels.Select(l => (n: l.Number, nl: l.NextLevels))
                .ToArray()
                .Should()
                .BeEquivalentTo(new[]
                                {
                                    (n: 0, new[] { 1 }),
                                    (n: 1, new[] { 2, 3 }),
                                    (n: 2, new int[0]),
                                    (n: 3, new int[0])
                                });
        }
    }
}