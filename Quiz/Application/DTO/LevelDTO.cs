using System;

namespace Application.DTO
{
    public class LevelDto<TId>
    {
        public int[] NextLevels { get; set; }
        public string Description { get; set; }
        public TId Id { get; set; }
        public TemplateTaskGeneratorDto[] Generators { get; set; }
    }

    public class LevelInsertionId
    {
        public Guid[] Previous { get; set; }
        public Guid[] Next { get; set; }
    }
    
}