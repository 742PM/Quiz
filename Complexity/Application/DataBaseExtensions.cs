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
                            LevelProgressEntities = topic
                                .Levels
                                .Take(1)
                                .ToDictionary(
                                    level => level.Id,
                                    level => level.ToProgressEntity())
                        })
            };
            return userRepository.FindById(userId) ?? userRepository.Insert(new UserEntity(userId, progress));
        }
    }
}