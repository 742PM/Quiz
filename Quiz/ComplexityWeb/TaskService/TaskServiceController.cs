using System;
using System.Collections.Generic;
using System.Linq;
using Application.TaskService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizWebApp.TaskService.DTO;

namespace QuizWebApp.TaskService
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
        ///     Получить список Topic.
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
        ///     Получить Level.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     GET service/0/level/1
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает уровень</response>
        [HttpGet("{topicId}/level/{levelId}")]
        public ActionResult<AdminLevelDTO> GetLevel(Guid topicId, Guid levelId)
        {
            var level = applicationApi.GetLevel(topicId, levelId);
            return Ok();
        }

        /// <summary>
        ///     Получить TemplateGenerator.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     GET service/0/1/templateGenerator/2
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает генератор</response>
        [HttpGet("{topicId}/{levelId}/templateGenerator/{generatorId}")]
        public ActionResult<AdminTaskGeneratorDTO> GetTemplateGenerator(Guid topicId, Guid levelId, Guid generatorId)
        {
            var generator = applicationApi.GetTemplateGenerator(topicId, levelId, generatorId);
            return Ok();
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
        [HttpPost("topic")]
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
        [HttpDelete("topic/{topicId}")]
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
        [HttpPost("{topicId}/level")]
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
        [HttpDelete("{topicId}/level/{levelId}")]
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
        [HttpPost("{topicId}/{levelId}/templateGenerator")]
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
        [HttpDelete("{topicId}/{levelId}/generator/{generatorId}")]
        public ActionResult DeleteGenerator(Guid topicId, Guid levelId, Guid generatorId)
        {
            applicationApi.DeleteGenerator(topicId, levelId, generatorId);
            return Ok();
        }

        /// <summary>
        ///     Рендерит и возвращает Task по шаблону из запроса
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/taskToRender
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
        [HttpPost("taskToRender")]
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