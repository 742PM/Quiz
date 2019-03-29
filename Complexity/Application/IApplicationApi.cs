using System;
using System.Collections.Generic;
using Application.Info;
using Infrastructure.Result;
using JetBrains.Annotations;

namespace Application
{
    public interface IApplicationApi
    {
        /// <summary>
        /// Получение списка названий тем
        /// </summary>
        /// <returns>
        /// Название и id темы
        /// </returns>
        Result<IEnumerable<TopicInfo>, Exception> GetTopicsInfo();

        /// <summary>
        /// Получение возможных уровней сложностей в данной теме 
        /// </summary>
        /// <returns>
        /// Уровни сложности
        /// </returns>
        Result<IEnumerable<LevelInfo>, Exception> GetLevels(Guid topicId);

        /// <summary>
        /// Получение доступных уровней сложности в данной теме для данного уровня 
        /// </summary>
        /// <returns>
        /// Доступные уровни сложности для пользователя
        /// </returns>
        Result<IEnumerable<LevelInfo>, Exception> GetAvailableLevels(Guid userId, Guid topicId);

        /// <summary>
        /// Прогресс пользователя в текущих теме и уровне
        /// </summary>
        /// <returns>
        /// Отношение решенных (набран полный стрик) задач ко всем
        /// </returns>
        Result<double, Exception> GetCurrentProgress(Guid userId, Guid topicId, Guid levelId);

        /// <summary>
        /// Получение задачи из конкретных темы и уровня
        /// </summary>
        /// <returns>
        /// Описание задачи с вариантами ответов
        /// </returns>
        Result<TaskInfo, Exception> GetTask(Guid userId, Guid topicId, Guid levelId);

        /// <summary>
        /// Получение следующей задачи из текущих темы и уровня
        /// </summary>
        /// <returns>
        /// Описание задачи с вариантами ответов
        /// </returns>
        Result<TaskInfo, Exception> GetNextTask(Guid userId);

        /// <summary>
        /// Проверка правильности ответа на текущую задачу
        /// </summary>
        Result<bool, Exception> CheckAnswer(Guid userId, string answer);

        /// <summary>
        /// Получение подсказки для текущей задачи
        /// </summary>
        Result<string, Exception> GetHint(Guid userId);
    }
}