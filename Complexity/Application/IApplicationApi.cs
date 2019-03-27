using System;
using System.Collections.Generic;
using Application.Info;
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
        IEnumerable<TopicInfo> GetTopicsInfo();

        /// <summary>
        /// Получение возможных уровней сложностей в данной теме 
        /// </summary>
        /// <returns>
        /// Уровни сложности
        /// </returns>
        IEnumerable<LevelInfo> GetLevels(Guid topicId);

        /// <summary>
        /// Получение доступных уровней сложности в данной теме для данного уровня 
        /// </summary>
        /// <returns>
        /// Доступные уровни сложности для пользователя
        /// </returns>
        IEnumerable<LevelInfo> GetAvailableLevels(Guid userId, Guid topicId);

        ///// <summary>
        ///// Прогресс пользователя в текущих теме и уровне сложности
        ///// </summary>
        ///// <returns>
        ///// 
        ///// </returns>
        //int GetCurrentProgress(Guid userId);

        /// <summary>
        /// Получение задачи из конкретных темы и уровня
        /// </summary>
        /// <returns>
        /// Описание задачи с вариантами ответов
        /// </returns>
        TaskInfo GetTask(Guid userId, Guid topicId, Guid levelId);

        /// <summary>
        /// Получение следующей задачи из текущих темы и уровня
        /// </summary>
        /// <returns>
        /// Описание задачи с вариантами ответов
        /// </returns>
        TaskInfo GetNextTask(Guid userId);

        /// <summary>
        /// Проверка правильности ответа на текущую задачу
        /// </summary>
        bool CheckAnswer(Guid userId, string answer);

        /// <summary>
        /// Получение подсказки для текущей задачи
        /// </summary>
        [CanBeNull]
        string GetHint(Guid userId);
    }
}