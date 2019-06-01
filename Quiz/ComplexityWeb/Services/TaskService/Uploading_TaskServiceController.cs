using System;
using System.IO;
using Application.DTO;
using Hjson;
using Infrastructure.Extensions;
using Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;
using static Hjson.HjsonValue;

namespace QuizWebApp.Services.TaskService
{
    public partial class TaskServiceController
    {
        //TODO: add authentication

        /// <summary>
        ///     Загружает тему в формате HJSON в БД, автоматически проставляя ей айди.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/for/humans/topics/upload
        ///     "{
        ///      name: Пустая тема
        ///      description: Очень пустая
        ///      levels: []
        ///      }"
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает айди топика, добавленного в БД</response>
        [HttpPost("for/humans/topics/upload")] //TODO: add authentication
        public ActionResult<Guid> UploadTopic([FromBody] string rawData) =>
            Parse(rawData)
                .ToString()
                .AndThen(j => j.Deserialize<TopicDto>())
                .AndThen(t => applicationApi.AddTopic(t))
                .Map(i => Ok(i))
                .Value;

        /// <summary>
        ///     Получить весь топик в формате HJSON без айди
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     GET service/for/humans/topics/4fab29a7-d536-4bad-aae1-d22a5255558c
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает топик в формате HJSON</response>
        /// <response code="404"> Id темы не найдены</response>
        [HttpGet("for/humans/topics/{topicId}")]
        public ActionResult<string> GetTopicHJson(Guid topicId)
        {
            var topic = applicationApi.GetFullTopic(topicId)
                                      .Select(t => t.Serialize())
                                      .Select(json => Load(new StringReader(json)))
                                      .Select(h => h.ToString(Stringify.Hjson));
            if (topic.HasNoValue)
                return NotFound();

            return Ok(topic.Value);
        }

        /// <summary>
        ///     Загружает тему в формате JSON в БД, автоматически проставляя ей айди.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/topics/upload
        ///     {
        ///        name: Пустая тема
        ///        description: Очень пустая
        ///        levels: []
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает айди топика, добавленного в БД</response>
        [HttpPost("topics/upload")]
        public ActionResult<Guid> UploadTopic([FromBody] TopicDto topic) =>
            topic.AndThen(t => applicationApi.AddTopic(t))
                 .Map(i => Ok(i))
                 .Value;

        /// <summary>
        ///     Получить тему без айди в нем
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     GET service/topics/4fab29a7-d536-4bad-aae1-d22a5255558c
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает тему в формате JSON</response>
        /// <response code="404"> Id темы не найдены</response>
        [HttpGet("topics/{topicId}")]
        public ActionResult<TopicDto> GetTopic(Guid topicId)
        {
            var topic = applicationApi.GetFullTopic(topicId);

            if (topic.HasNoValue)
                return NotFound();

            return Ok(topic.Value);
        }
    }
}