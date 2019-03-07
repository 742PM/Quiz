using System;
using System.Collections.Generic;
using Domain;

namespace Application
{
    public interface IApi
    {
        Topic GetTopicById(Guid topicId);
        Level GetLevelById(Guid userId, Guid topicId, Guid levelId);
        Task GetTaskById(Guid userId, Guid topicId, Guid levelId, Guid taskId);

        bool IsRightAnswerForLastTask(Guid userId, string answer);
        Task GetNextTask(Guid userId);

        IEnumerable<Topic> GetAllTopics();
        IEnumerable<Level> GetAvailableLevelsForUser(Guid userId, Guid topicId);
        IEnumerable<Task> GetAvailableTasksForUser(Guid userId, Guid topicId, Guid levelId);

        bool IsTopicCompleted(Guid userId, Guid topicId);
        bool IsLevelCompleted(Guid userId, Guid topicId, Guid levelId);
        bool IsTaskCompleted(Guid userId, Guid topicId, Guid levelId, Guid taskId);
    }
}