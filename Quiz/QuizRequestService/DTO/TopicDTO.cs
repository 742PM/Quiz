using System;

namespace QuizRequestService.DTO
{
    public class TopicDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TopicDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}