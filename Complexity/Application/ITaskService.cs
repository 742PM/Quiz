using System;
using System.Collections.Generic;
using Domain.Values;
using Infrastructure.Result;

namespace Application
{
    public interface ITaskService
    {
        /// <summary>
        ///     Получить все темы из базы данных
        /// </summary>
        IEnumerable<Task> GetAllTopics();

        /// <summary>
        ///     Добавить пустую тему в базу данных
        /// </summary>
        /// <param name="name">Имя темы</param>
        /// <param name="description">Описание темы</param>
        /// <returns>Id добавленной темы</returns>
        Guid AddEmptyTopic(string name, string description);

        /// <summary>
        ///     Добавить пустой уровень в базу данных
        /// </summary>
        /// <param name="topicId">Id темы, в которую добавится уровень</param>
        /// <param name="description">Описание уровня</param>
        /// <param name="previousLevels">Список Id предыдущих уровней</param>
        /// <param name="nextLevels">Список Id следующих уровней</param>
        /// <returns>Id добавленного уровня</returns>
        /// <exception cref="ArgumentException">Id темы не найден</exception>
        Result<Guid, Exception> AddEmptyLevel(
            Guid topicId,
            string description,
            IEnumerable<Guid> previousLevels,
            IEnumerable<Guid> nextLevels);

        /// <summary>
        ///     Добавить шаблонный генератор в базу данных
        /// </summary>
        /// <param name="topicId">Id темы, в которую добавится генератор</param>
        /// <param name="levelId">Id уровня, в который добавится генератор</param>
        /// <param name="template">Шаблон вопроса</param>
        /// <param name="possibleAnswers">Список возможных ответов (включая правильный)</param>
        /// <param name="rightAnswer">Правильный ответ</param>
        /// <param name="hints">Список подсказок</param>
        /// <param name="streak">Необходимое количество подряд правильно решенных задач для прохождения</param>
        /// <returns>Id добавленного генератора</returns>
        /// <exception cref="ArgumentException">Id темы или уровня не найдены</exception>
        Guid AddTemplateGenerator(
            Guid topicId,
            Guid levelId,
            string template,
            IEnumerable<string> possibleAnswers,
            string rightAnswer,
            IEnumerable<string> hints,
            int streak);

        /// <summary>
        ///     Получить задачу, созданную генератором с данными аргументами 
        /// </summary>
        /// <param name="template">Шаблон вопроса</param>
        /// <param name="possibleAnswers">Список возможных ответов (включая правильный)</param>
        /// <param name="rightAnswer">Правильный ответ</param>
        /// <param name="hints">Список подсказок</param>
        /// <returns>Сгенерированная задача</returns>
        Task RenderTask(
            string template,
            IEnumerable<string> possibleAnswers,
            string rightAnswer,
            IEnumerable<string> hints);
    }
}