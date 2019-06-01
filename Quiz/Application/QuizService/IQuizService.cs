using System;
using System.Collections.Generic;
using Application.Exceptions;
using Application.Info;
using Domain.Entities;
using Infrastructure.Result;

namespace Application.QuizService
{
    public interface IQuizService
    {
        /// <summary>
        ///     Получение списка тем
        /// </summary>
        Result<IEnumerable<TopicInfo>, Exception> GetTopicsInfo();

        /// <summary>
        ///     Получение возможных уровней сложностей в данной теме
        /// </summary>
        /// <exception cref="ArgumentException">Неизвестный id темы</exception>
        Result<IEnumerable<LevelInfo>, Exception> GetLevels(Guid topicId);

        /// <summary>
        ///     Получение доступных уровней сложности в данной теме для данного уровня
        /// </summary>
        /// <exception cref="ArgumentException">Неизвестный id темы</exception>
        Result<IEnumerable<LevelInfo>, Exception> GetAvailableLevels(Guid userId, Guid topicId);

        /// <summary>
        ///     Прогресс пользователя в текущих теме и уровне
        /// </summary>
        /// <exception cref="ArgumentException">Неизвестный id темы или уровня</exception>
        /// <exception cref="AccessDeniedException">Метод недоступен для данного пользователя</exception>
        Result<LevelProgressInfo, Exception> GetProgress(Guid userId, Guid topicId, Guid levelId);

        /// <summary>
        ///     Получение задачи из конкретных темы и уровня
        /// </summary>
        /// <exception cref="ArgumentException">Неизвестный id темы или уровня</exception>
        /// <exception cref="ArgumentOutOfRangeException">Отсутствуют генераторы</exception>
        /// <exception cref="AccessDeniedException">Метод недоступен для данного пользователя</exception>
        Result<TaskInfo, Exception> GetTask(Guid userId, Guid topicId, Guid levelId);

        /// <summary>
        ///     Получение следующей задачи из текущих темы и уровня
        /// </summary>
        /// <exception cref="AccessDeniedException">Метод недоступен для данного пользователя</exception>
        /// <exception cref="ArgumentOutOfRangeException">Отсутствуют генераторы</exception>
        Result<TaskInfo, Exception> GetNextTask(Guid userId);

        /// <summary>
        ///     Проверка правильности ответа на текущую задачу
        /// </summary>
        /// <exception cref="AccessDeniedException">Метод недоступен для данного пользователя</exception>
        Result<bool, Exception> CheckAnswer(Guid userId, string answer);

        /// <summary>
        ///     Получение подсказки для текущей задачи
        /// </summary>
        /// <exception cref="AccessDeniedException">Метод недоступен для данного пользователя</exception>
        /// <exception cref="OutOfHintsException">Подсказки отсутствуют или кончились</exception>
        Result<HintInfo, Exception> GetHint(Guid userId);
    }
}
