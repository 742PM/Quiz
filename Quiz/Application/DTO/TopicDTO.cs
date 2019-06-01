using System;
using System.Linq;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using Infrastructure.Extensions;
using static System.Guid;

namespace Application.DTO
{
    public class TopicDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public LevelDto[] Levels { get; set; }

        public static explicit operator Topic(TopicDto dto)
        {
            var ids = dto.Levels.Select(_ => NewGuid()).ToArray();
            return new Topic(NewGuid(),
                dto.Name,
                dto.Description,
                dto.Levels
                    .Select(levelDto =>
                        new Level(
                            ids[levelDto.Number],
                            levelDto.Description,
                            levelDto.Generators
                                .Select(generatorDto => (TaskGenerator)generatorDto)
                                .ToArray(),
                            levelDto.NextLevels.Select(i => ids[i]).ToArray()))
                    .ToArray());
        }

        public static explicit operator TopicDto(Topic topic)
        {
            var ids = topic.Levels
                .Select((l, i) => (level: l, i))
                .ToDictionary(tup => tup.level.Id, tup => tup.i);

            return new TopicDto
            {
                Description = topic.Description,
                Name = topic.Name,
                Levels = topic.Levels.Select(level =>
                        new LevelDto
                        {
                            Description = level.Description,
                            Generators = level
                                .Generators
                                .Select(g => (TemplateTaskGeneratorDto)g
                                    .Cast<TemplateTaskGenerator>())
                                .ToArray(),
                            NextLevels = level
                                .NextLevels.Select(id => ids[id])
                                .ToArray(),
                            Number = ids[level.Id]
                        }
                    )
                    .ToArray()
            };
        }
    }
}