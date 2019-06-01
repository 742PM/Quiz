using System;
using System.Collections.Generic;
using Application.DTO;
using Domain.Entities;
using Domain.Entities.TaskGenerators;
using Domain.Values;
using Infrastructure;
using Infrastructure.Result;

namespace Application.TaskService
{
    public interface ITaskService
    {
        /// <summary>
        ///     Получить генераторы из базы данных
        /// </summary>
        Result<IEnumerable<TemplateTaskGenerator>, Exception> GetTemplateGenerators(Guid topicId, Guid levelId);

        /// <summary>
        ///     Добавить пустую тему в базу данных
        /// </summary>
        /// <param name="name">Имя темы</param>
        /// <param name="description">Описание темы</param>
        /// <returns>Id добавленной темы</returns>
        Guid AddEmptyTopic(string name, string description);

        /// <summary>
        ///     Удалить тему из базы данных
        /// </summary>
        /// <param name="topicId">Id удаляемой темы</param>
        /// <exception cref="ArgumentException">Id темы не найден</exception>
        Result<None, Exception> DeleteTopic(Guid topicId);

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
        ///     Удалить уровень из базы данных
        /// </summary>
        /// <param name="topicId">Id удаляемой темы</param>
        /// <param name="levelId">Id удаляемого уровня</param>
        /// <exception cref="ArgumentException">Id темы или уровня не найдены</exception>
        Result<None, Exception> DeleteLevel(Guid topicId, Guid levelId);

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
        /// <param name="question">Вопрос</param>
        /// <returns>Id добавленного генератора</returns>
        /// <exception cref="ArgumentException">Id темы или уровня не найдены</exception>
        Result<Guid, Exception> AddTemplateGenerator(
            Guid topicId,
            Guid levelId,
            string template,
            IEnumerable<string> possibleAnswers,
            string rightAnswer,
            IEnumerable<string> hints,
            int streak,
            string question);

        /// <summary>
        ///     Удалить генератор из базы данных
        /// </summary>
        /// <param name="topicId">Id удаляемой темы</param>
        /// <param name="levelId">Id удаляемого уровня</param>
        /// <param name="generatorId">Id удаляемого генератора</param>
        /// <exception cref="ArgumentException">Id темы, уровня или генератора не найдены</exception>
        Result<None, Exception> DeleteGenerator(Guid topicId, Guid levelId, Guid generatorId);

        /// <summary>
        ///     Получить задачу, созданную генератором с данными аргументами 
        /// </summary>
        /// <param name="template">Шаблон вопроса</param>
        /// <param name="possibleAnswers">Список возможных ответов (включая правильный)</param>
        /// <param name="rightAnswer">Правильный ответ</param>
        /// <param name="hints">Список подсказок</param>
        /// <param name="question">Вопрос</param>
        /// <returns>Сгенерированная задача</returns>
        Task RenderTask(
            string template,
            IEnumerable<string> possibleAnswers,
            string rightAnswer,
            IEnumerable<string> hints,
            string question);

        /// <summary>
        /// Получить всю тему по айди.
        /// </summary>
        /// <param name="topicId">айди темы</param>
        /// <returns>Всю сущность темы целиком</returns>
        [Unsafe]
        Result<TopicDto, Exception> GetFullTopic(Guid topicId);

        /// <summary>
        /// Добавляет указанную тему c новым айди
        /// </summary>
        /// <param name="topic">тема для добавления</param>
        /// <returns>в случае успеха айди добавленной темы</returns>
        [Unsafe]
        Guid AddTopic(TopicDto topic);

    }
}