using System.Linq;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
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
            var ids = dto.Levels.ToDictionary(l => l.Number, _ => NewGuid());
            return new Topic(NewGuid(), dto.Name, dto.Description,
                             dto.Levels.Select(levelDto => new Level(ids[levelDto.Number], levelDto.Description,
                                                                     levelDto.Generators
                                                                             .Select(generatorDto =>
                                                                                         (TaskGenerator)
                                                                                         (TemplateTaskGenerator)
                                                                                         generatorDto)
                                                                             .ToArray(),
                                                                     levelDto.NextLevels.Select(i => ids[i]).ToArray()))
                                .ToArray());
        }
    }
}