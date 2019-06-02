using System;
using System.IO;
using Application.DTO;
using Hjson;
using Infrastructure.Extensions;
using Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;
using QuizWebApp.Extensions;
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
        [HttpPost("hjsonTopic")] //TODO: add authentication
        public ActionResult<Guid> UploadTopic([FromBody] string rawData)
        {
            return Parse(rawData)
                .ToString()
                .AndThen(j => j.Deserialize<TopicDto>())
                .AndThen(t => applicationApi.AddTopic(t))
                .AndThen(i => Ok(i));
        }

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
        [HttpGet("{topicId}/hjsonTopic")]
        public ActionResult<string> GetTopicHJson(Guid topicId)
        {
            var (_, isFailure, value, error) =
                applicationApi.GetFullTopic(topicId)
                    .Map(t => t.Serialize())
                    .Map(json => Load(new StringReader(json)))
                    .Map(h => h.ToString(Stringify.Hjson));
            if (isFailure)
                return NotFound(error.Message);

            return Ok(value);
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
        [HttpPost("jsonTopic")]
        public ActionResult<Guid> UploadTopic([FromBody] TopicDto topic)
        {
            return topic.AndThen(t => applicationApi.AddTopic(t))
                .AndThen(i => Ok(i));
        }

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
        [HttpGet("{topicId}/jsonTopic")]
        public ActionResult<TopicDto> GetTopic(Guid topicId)
        {
            var (_, isFailure, topic, error) = applicationApi.GetFullTopic(topicId);

            if (isFailure)
                return NotFound(error.Message);

            return Ok(topic);
        }
    }
}