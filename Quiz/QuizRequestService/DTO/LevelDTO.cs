using System;

namespace QuizRequestService.DTO
{
    public class LevelDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public LevelDTO(Guid id, string name)
        {
            Id = id;
            Description = name;
        }
    }
}