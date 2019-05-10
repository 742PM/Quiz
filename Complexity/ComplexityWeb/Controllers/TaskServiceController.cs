using System;
using System.Collections.Generic;
using System.Linq;
using Application.TaskService;
using AutoMapper;
using ComplexityWebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ComplexityWebApi.Controllers
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
        public ActionResult<IEnumerable<TopicInfoDTO>> GetTopics()
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
        public ActionResult<Guid> AddEmptyTopic([FromBody] TopicWithDescriptionDTO topic)
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
            var (_, _) = applicationApi.DeleteTopic(topicId);
            return Ok();
        }

        /// <summary>
        ///     Добавляет в сервис новый пустой Level.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/addLevel/0
        ///     {
        ///         "description": "Оценка сложностей алгоритмов",
        ///         "next_levels": [0, 1],
        ///         "previous_levels": [2, 3]
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает Guid от нового Level</response>
        [HttpPost("addLevel/{topicId}")]
        public ActionResult<Guid> AddLevel(Guid topicId, [FromBody] DataBaseLevelDTO level)
        {
            var (levelGuid, _) = applicationApi.AddEmptyLevel(topicId, level.Description, level.PreviousLevels, level.NextLevels);
            return Ok(levelGuid);
        }

        /// <summary>
        ///     Удаляет Level из сервиса.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     DELETE service/deleteLevel/1/0
        ///     </code>
        /// </remarks>
        /// <response code="200"> Level был удален</response>
        [HttpDelete("deleteLevel/{topicId}/{levelId}")]
        public ActionResult DeleteLevel(Guid topicId, Guid levelId)
        {
            var (_, _) = applicationApi.DeleteLevel(topicId, levelId);
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
        ///        "template": "for (int i = {{from1}}; i &lt; {{to1}}; i += {{iter1}})\r\nc++\r\n",
        ///        "possibleAnswers": ["Θ(1)", "Θ(log(n))"],
        ///        "rightAnswer": "Θ(n)",
        ///        "hints": [],
        ///        "streak": 1
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает Guid от нового TemplateGenerator</response>
        [HttpPost("addTemplateGenerator/{topicId}/{levelId}")]
        public ActionResult<Guid> AddTemplateGenerator(Guid topicId, Guid levelId, [FromBody] DataBaseTemplateGeneratorWithStreakDTO templateGenerator)
        {
            var (generatorGuid, _) = applicationApi.AddTemplateGenerator(topicId, levelId, templateGenerator.Template, templateGenerator.PossibleAnswers,
                templateGenerator.RightAnswer, templateGenerator.Hints, templateGenerator.Streak);
            return Ok(generatorGuid);
        }
        
        /// <summary>
        ///     Удаляет Generator из сервиса.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     DELETE service/deleteGenerator/1/0/2
        ///     </code>
        /// </remarks>
        /// <response code="200"> Generator был удален</response>
        [HttpDelete("deleteGenerator/{topicId}/{levelId}/{generatorId}")]
        public ActionResult DeleteGenerator(Guid topicId, Guid levelId, Guid generatorId)
        {
            var (_, _) = applicationApi.DeleteGenerator(topicId, levelId, generatorId);
            return Ok();
        }

        /// <summary>
        ///     Рендерит и возвращает Task по шаблону полученому в запросе
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/renderTask
        ///     {
        ///        "template": "for (int i = {{from1}}; i &lt; {{to1}}; i += {{iter1}})\r\nc++\r\n",
        ///        "possibleAnswers": ["Θ(1)", "Θ(log(n))"],
        ///        "rightAnswer": "Θ(n)",
        ///        "hints": []
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает отрендереный Task</response>
        [HttpPost("renderTask")]
        public ActionResult RenderTask([FromBody] DataBaseTemplateGeneratorDTO templateGenerator)
        {
            var task = applicationApi.RenderTask(templateGenerator.Template, templateGenerator.PossibleAnswers,
                templateGenerator.RightAnswer, templateGenerator.Hints);

            return Ok(task);
        }
    }
}