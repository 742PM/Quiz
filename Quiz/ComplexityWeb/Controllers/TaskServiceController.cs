using System;
using System.Collections.Generic;
using System.Linq;
using Application.TaskService;
using AutoMapper;
using ComplexityWebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using QuizWebApp.DTO;

namespace QuizWebApp.Controllers
{
    [Route("service")]
    [ApiController]
    public class TaskServiceController : ControllerBase
    {
        private readonly ITaskService applicationApi;

        public TaskServiceController(ITaskService applicationApi)
        {
            this.applicationApi = applicationApi;
        }

        /// <summary>
        ///     Получить список всех Topic.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     GET service/topics
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает список тем</response>
        [HttpGet("topics")]
        public ActionResult<IEnumerable<AdminTopicDTO>> GetTopics()
        {
            var topics = applicationApi.GetAllTopics();
            return Ok(topics.Select(Mapper.Map<AdminTopicDTO>));
        }

        /// <summary>
        ///     Добавляет в сервис новый пустой Topic.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/addTopic
        ///     {
        ///         "name": "Сложность алгоритмов",
        ///         "description": "Оценка сложностей алгоритмов"
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает Guid от нового Topic</response>
        [HttpPost("addTopic")]
        public ActionResult<Guid> AddEmptyTopic([FromBody] EmptyTopicDTO topic)
        {
            var topicGuid = applicationApi.AddEmptyTopic(topic.Name, topic.Description);
            return Ok(topicGuid);
        }

        /// <summary>
        ///     Удаляет Topic из сервиса.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     DELETE service/deleteTopic/1
        ///     </code>
        /// </remarks>
        /// <response code="200"> Topic был удален</response>
        [HttpDelete("deleteTopic/{topicId}")]
        public ActionResult DeleteTopic(Guid topicId)
        {
            applicationApi.DeleteTopic(topicId);
            return Ok();
        }

        /// <summary>
        ///     Добавляет в сервис новый пустой Level.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/0/addLevel
        ///     {
        ///         "description": "Оценка сложностей алгоритмов",
        ///         "nextLevels": [0, 1],
        ///         "previousLevels": [2, 3]
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает Guid от нового Level</response>
        [HttpPost("{topicId}/addLevel")]
        public ActionResult<Guid> AddLevel(Guid topicId, [FromBody] EmptyLevelDTO level)
        {
            var levelGuid = applicationApi
                .AddEmptyLevel(topicId, level.Description, level.PreviousLevels, level.NextLevels)
                .Value;
            return Ok(levelGuid);
        }

        /// <summary>
        ///     Удаляет Level из сервиса.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     DELETE service/1/deleteLevel/1
        ///     </code>
        /// </remarks>
        /// <response code="200"> Level был удален</response>
        [HttpDelete("{topicId}/deleteLevel/{levelId}")]
        public ActionResult DeleteLevel(Guid topicId, Guid levelId)
        {
            applicationApi.DeleteLevel(topicId, levelId);
            return Ok();
        }

        /// <summary>
        ///     Добавляет в сервис новый TemplateGenerator.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/addTemplateGenerator/1/0
        ///     {
        ///        "question": "Оцените временную сложность алгоритма",
        ///        "text": "for (int i = {{from1}}; i &lt; {{to1}}; i += {{iter1}})\r\nc++\r\n",
        ///        "possibleAnswers": ["Θ(1)", "Θ(log(n))"],
        ///        "answer": "Θ(n)",
        ///        "hints": [],
        ///        "streak": 1
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает Guid от нового TemplateGenerator</response>
        [HttpPost("{topicId}/{levelId}addTemplateGenerator")]
        public ActionResult<Guid> AddTemplateGenerator(
            Guid topicId,
            Guid levelId,
            [FromBody] TemplateGeneratorDTO templateGenerator)
        {
            var (generatorGuid, _) = applicationApi.AddTemplateGenerator(topicId, levelId, templateGenerator.Text,
                templateGenerator.PossibleAnswers,
                templateGenerator.Answer, templateGenerator.Hints, templateGenerator.Streak,
                templateGenerator.Question);
            return Ok(generatorGuid);
        }

        /// <summary>
        ///     Удаляет Generator из сервиса.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     DELETE service/1/0/deleteGenerator/2
        ///     </code>
        /// </remarks>
        /// <response code="200"> Generator был удален</response>
        [HttpDelete("{topicId}/{levelId}/deleteGenerator/{generatorId}")]
        public ActionResult DeleteGenerator(Guid topicId, Guid levelId, Guid generatorId)
        {
            applicationApi.DeleteGenerator(topicId, levelId, generatorId);
            return Ok();
        }

        /// <summary>
        ///     Рендерит и возвращает Task по шаблону, полученному в запросе
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/renderTask
        ///     {
        ///        "question": "Оцените временную сложность алгоритма",
        ///        "text": "for (int i = {{from1}}; i &lt; {{to1}}; i += {{iter1}})\r\nc++\r\n",
        ///        "possibleAnswers": ["Θ(1)", "Θ(log(n))"],
        ///        "answer": "Θ(n)",
        ///        "hints": []
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает отрендереный Task</response>
        [HttpPost("renderTask")]
        public ActionResult RenderTask([FromBody] TemplateGeneratorForRenderDTO templateGenerator)
        {
            var task = applicationApi.RenderTask(
                templateGenerator.Text,
                templateGenerator.PossibleAnswers,
                templateGenerator.Answer,
                templateGenerator.Hints,
                templateGenerator.Question);

            return Ok(task);
        }
    }
}