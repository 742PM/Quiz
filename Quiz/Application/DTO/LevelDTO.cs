using System;

namespace Application.DTO {
    public class LevelDto
    {
        public int[] NextLevels { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public TemplateTaskGeneratorDTO[] Generators { get; set; }
    }
}

