using System;

namespace Application.DTO
{
    public class LevelDto
    {
        public int[] NextLevels { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public TemplateTaskGeneratorDto[] Generators { get; set; }

        public LevelDto(int number, string description, TemplateTaskGeneratorDto[] generators, int[] nextLevels)
        {
            NextLevels = nextLevels;
            Description = description;
            Number = number;
            Generators = generators;
        }

        public LevelDto()
        {
            
        }
    }
}