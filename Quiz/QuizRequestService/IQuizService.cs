using System;
using System.Collections.Generic;
using QuizRequestService.DTO;

namespace QuizRequestService
{
    public interface IQuizService
    {
        IEnumerable<TopicDTO> GetTopics();

        IEnumerable<LevelDTO> GetLevels(Guid topicId);

        IEnumerable<LevelDTO> GetAvailableLevels(Guid userId, Guid topicId);

        ProgressDTO GetProgress(Guid userId, Guid topicId, Guid levelId);

        TaskDTO GetTaskInfo(Guid userId, Guid topicId, Guid levelId);

        TaskDTO GetNextTaskInfo(Guid userId);

        HintDTO GetHint(Guid userId);

        bool? SendAnswer(Guid userId, string answer);
    }
}
