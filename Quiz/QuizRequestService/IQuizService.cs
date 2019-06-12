using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Result;
using QuizRequestService.DTO;

namespace QuizRequestService
{
    public interface IQuizService
    {
        Task<Maybe<IEnumerable<TopicDTO>>> GetTopics();

        Task<Maybe<IEnumerable<LevelDTO>>> GetLevels(Guid topicId);

        Task<Maybe<IEnumerable<LevelDTO>>> GetAvailableLevels(Guid userId, Guid topicId);

        Task<Maybe<ProgressDTO>> GetProgress(Guid userId, Guid topicId, Guid levelId);

        Task<Maybe<TaskDTO>> GetTaskInfo(Guid userId, Guid topicId, Guid levelId);

        Task<Maybe<TaskDTO>> GetNextTaskInfo(Guid userId);

        Task<Maybe<HintDTO>> GetHint(Guid userId);

        Task<Maybe<bool>> SendAnswer(Guid userId, string answer);
    }
}
