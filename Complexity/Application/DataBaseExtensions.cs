using System;
using System.Linq;
using DataBase;
using DataBase.Entities;

namespace Application
{
    public static class DataBaseExtensions
    {
        public static UserEntity FindOrInsertUser(this IUserRepository userRepository, Guid userId, ITaskRepository taskRepository)
        {
            var progress = new UserProgressEntity
            {
                TopicsProgress = taskRepository
                    .GetTopics()
                    .ToDictionary(
                        topic => topic.Id,
                        topic => new TopicProgressEntity
                        {
                            TopicId = topic.Id,
                            LevelProgressEntities = taskRepository
                                .GetLevelsFromTopic(topic.Id)
                                .Take(1)
                                .ToDictionary(
                                    level => level.Id,
                                    level => taskRepository.GetLevelProgressEntity(level.Id, topic.Id))
                        })
            };
            return userRepository.FindById(userId) ?? userRepository.Insert(new UserEntity(userId, progress));
        }

        public static LevelProgressEntity GetLevelProgressEntity(this ITaskRepository taskRepository, Guid levelId, Guid topicId)
        {
            return new LevelProgressEntity
            {
                LevelId = levelId,
                CurrentLevelStreaks = taskRepository
                    .GetGeneratorsFromLevel(topicId, levelId)
                    .ToDictionary(
                        generator => generator.Id,
                        generator => 0)
            };
        }
    }
}