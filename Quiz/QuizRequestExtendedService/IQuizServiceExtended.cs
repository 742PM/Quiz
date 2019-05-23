using System;
using System.Collections.Generic;
using QuizRequestExtendedService.DTO;

namespace QuizRequestExtendedService
{
    public interface IQuizServiceExtended
    {
        IEnumerable<TopicDTO> GetTopics();
        IEnumerable<LevelDTO> GetLevels(Guid topicId);
        IEnumerable<AdminTemplateGeneratorDTO> GetTemplateGenerators(Guid topicId, Guid levelId);
        Guid AddEmptyTopic(EmptyTopicDTO topic);
        void DeleteTopic(Guid topicId);
        TemplateGeneratorDTO RenderTask(TemplateGeneratorForRenderDTO templateGenerator);
    }
}