

using System;
using Domain.Entities.TaskGenerators;

namespace Application.DTO {
    public class TemplateTaskGeneratorDTO
    {
        public string[] PossibleAnswers { get; set; }

        public string Text { get; set; }

        public string Question { get; set; }

        public string[] Hints { get; set; }

        /// <summary>
        ///     Should not be used as real answer for user;
        /// </summary>
        public string Answer { get; set; }
        public int Streak { get; set; }

        public static explicit  operator TemplateTaskGeneratorDTO (TemplateTaskGenerator generator) =>
            new TemplateTaskGeneratorDTO
            {
                Answer = generator.Answer,
                Hints = generator.Hints,
                PossibleAnswers = generator.PossibleAnswers,
                Question = generator.Question,
                Text = generator.Text,
                Streak = generator.Streak
            };

        public static explicit operator TemplateTaskGenerator(TemplateTaskGeneratorDTO dto)
            => new TemplateTaskGenerator(Guid.NewGuid(), dto.PossibleAnswers, dto.Text, dto.Hints, dto.Answer, dto.Streak, dto.Question);

    }
}