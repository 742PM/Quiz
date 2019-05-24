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

        Guid AddEmptyLevel(Guid topicId, EmptyLevelDTO level);

        void DeleteLevel(Guid topicId, Guid levelId);

        Guid AddEmptyGenerator(Guid topicId, Guid levelId, TemplateGeneratorDTO topic);

        void DeleteTemplateGenerator(Guid topicId, Guid levelId, Guid generatorId);

        TemplateGeneratorDTO RenderTask(TemplateGeneratorForRenderDTO templateGenerator);
    }
}