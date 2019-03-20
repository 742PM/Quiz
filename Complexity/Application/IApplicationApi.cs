using System;
using System.Collections.Generic;
using Domain;

namespace Application
{
    public interface IApplicationApi
    {
        /// <summary>
        /// Поулчение списка названий тем
        /// </summary>
        IEnumerable<string> GetTopicNames();

        /// <summary>
        /// Получение массива описаний генераторов по каждому доступному уровню сложности для данного юзера из данной темы
        /// </summary>
        /// <param name="userId">id юзера для получения доступных сложностей</param>
        /// <param name="topicId">id темы, сложности которой мы запрашиваем</param>
        /// <returns>
        /// Массив перечислений описаий генераторов по доступным уровням сложности для данной темы
        /// индекс массива - уровень сложности
        /// элемент перечисления - описание генератора
        /// </returns>
        IEnumerable<string>[] GetAvailableDifficulties(Guid userId, Guid topicId);

        /// <summary>
        /// Прогресс пользователя в текущих теме и уровне сложности
        /// </summary>
        /// <returns>прогресс в ?процентах?</returns>
        int GetCurrentProgress(Guid userId);

        /// <summary>
        /// Получение задачи из конкретной темы по данной сложности
        /// </summary>
        /// <returns>Описание задачи с вариантами ответов</returns>
        TaskDescription GetTask(Guid userId, Guid topicId, int difficulty);

        /// <summary>
        /// Получение следующей задачи из текущих темы и уровня сложности
        /// </summary>
        /// <returns>Описание задачи с вариантами ответов</returns>
        TaskDescription GetNextTask(Guid userId);

        /// <summary>
        /// Получение подобной задачи от текущего генератора
        /// </summary>
        /// <returns>Описание задачи с вариантами ответов</returns>
        TaskDescription GetSimilarTask(Guid userId);

        /// <summary>
        /// Проверка правльности ответа на текущую задачу
        /// </summary>
        bool CheckAnswer(Guid userId, string answer);

        /// <summary>
        /// Получение подсказки для текущей задачи
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetHint(Guid userId);
    }
}