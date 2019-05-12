using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Repositories.Entities;
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
        private readonly IUserRepository userRepository;

        public TaskServiceController(ITaskService applicationApi, IUserRepository userRepository)
        {
            this.applicationApi = applicationApi;
            this.userRepository = userRepository;
        }

        /// <summary>
        ///     Получить список всех Topic.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     GET service/0/topics
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает список тем</response>
        [HttpGet("{userId}/topics")]
        public ActionResult<IEnumerable<AdminTopicDTO>> GetTopics(Guid userId)
        {
            var rights = GetRightsById(userId);
            if (!rights.CanGetGeneratorsWithLevels)
                return Forbid();

            var topics = applicationApi.GetAllTopics();

            return Ok(topics.Select(Mapper.Map<AdminTopicDTO>));
        }

        /// <summary>
        ///     Добавляет в сервис новый пустой Topic.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/0/addTopic
        ///     {
        ///         "name": "Сложность алгоритмов",
        ///         "description": "Оценка сложностей алгоритмов"
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает Guid от нового Topic</response>
        [HttpPost("{userId}/addTopic")]
        public ActionResult<Guid> AddEmptyTopic([FromBody] TopicWithDescriptionDTO topic, Guid userId)
        {
            var rights = GetRightsById(userId);
            if (!rights.CanEditTopics)
                return Forbid();

            var topicGuid = applicationApi.AddEmptyTopic(topic.Name, topic.Description);

            return Ok(topicGuid);
        }

        /// <summary>
        ///     Удаляет Topic из сервиса.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     DELETE service/0/deleteTopic/1
        ///     </code>
        /// </remarks>
        /// <response code="200"> Topic был удален</response>
        [HttpDelete("{userId}/deleteTopic/{topicId}")]
        public ActionResult DeleteTopic(Guid userId, Guid topicId)
        {
            var rights = GetRightsById(userId);
            if (!rights.CanEditTopics)
                return Forbid();

            var (_, _) = applicationApi.DeleteTopic(topicId);
            return Ok();
        }

        /// <summary>
        ///     Добавляет в сервис новый пустой Level.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/0/addLevel/0
        ///     {
        ///         "description": "Оценка сложностей алгоритмов",
        ///         "next_levels": [0, 1],
        ///         "previous_levels": [2, 3]
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает Guid от нового Level</response>
        [HttpPost("{userId}/addLevel/{topicId}")]
        public ActionResult<Guid> AddLevel(Guid userId, Guid topicId, [FromBody] DataBaseLevelDTO level)
        {
            var rights = GetRightsById(userId);
            if (!rights.CanEditLevels)
                return Forbid();

            var (levelGuid, _) =
                applicationApi.AddEmptyLevel(topicId, level.Description, level.PreviousLevels, level.NextLevels);
            return Ok(levelGuid);
        }

        /// <summary>
        ///     Удаляет Level из сервиса.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     DELETE service/0/deleteLevel/1/0
        ///     </code>
        /// </remarks>
        /// <response code="200"> Level был удален</response>
        [HttpDelete("{userId}/deleteLevel/{topicId}/{levelId}")]
        public ActionResult DeleteLevel(Guid userId, Guid topicId, Guid levelId)
        {
            var rights = GetRightsById(userId);
            if (!rights.CanEditLevels)
                return Forbid();

            var (_, _) = applicationApi.DeleteLevel(topicId, levelId);
            return Ok();
        }

        /// <summary>
        ///     Добавляет в сервис новый TemplateGenerator.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/0/addTemplateGenerator/1/0
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
        [HttpPost("{userId}/addTemplateGenerator/{topicId}/{levelId}")]
        public ActionResult<Guid> AddTemplateGenerator(Guid userId, Guid topicId, Guid levelId,
            [FromBody] DataBaseTemplateGeneratorWithStreakDTO templateGenerator)
        {
            var rights = GetRightsById(userId);
            if (!rights.CanEditGenerators)
                return Forbid();

            var (generatorGuid, _) = applicationApi.AddTemplateGenerator(topicId, levelId, templateGenerator.Template,
                templateGenerator.PossibleAnswers,
                templateGenerator.RightAnswer, templateGenerator.Hints, templateGenerator.Streak,
                templateGenerator.Question);
            return Ok(generatorGuid);
        }

        /// <summary>
        ///     Удаляет Generator из сервиса.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     DELETE service/0/deleteGenerator/1/0/2
        ///     </code>
        /// </remarks>
        /// <response code="200"> Generator был удален</response>
        [HttpDelete("{userId}/deleteGenerator/{topicId}/{levelId}/{generatorId}")]
        public ActionResult DeleteGenerator(Guid userId, Guid topicId, Guid levelId, Guid generatorId)
        {
            var rights = GetRightsById(userId);
            if (!rights.CanEditGenerators)
                return Forbid();
            var (_, _) = applicationApi.DeleteGenerator(topicId, levelId, generatorId);
            return Ok();
        }

        /// <summary>
        ///     Рендерит и возвращает Task по шаблону полученому в запросе
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST service/0/renderTask
        ///     {
        ///        "template": "for (int i = {{from1}}; i &lt; {{to1}}; i += {{iter1}})\r\nc++\r\n",
        ///        "possibleAnswers": ["Θ(1)", "Θ(log(n))"],
        ///        "rightAnswer": "Θ(n)",
        ///        "hints": []
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает отрендереный Task</response>
        [HttpPost("{userId}/renderTask")]
        public ActionResult RenderTask(Guid userId, [FromBody] DataBaseTemplateGeneratorDTO templateGenerator)
        {
            var rights = GetRightsById(userId);
            if (!rights.CanRenderTasks)
                return Forbid();

            var task = applicationApi.RenderTask(templateGenerator.Template, templateGenerator.PossibleAnswers,
                templateGenerator.RightAnswer, templateGenerator.Hints, templateGenerator.Question);

            return Ok(task);
        }

        private UserRightsEntity GetRightsById(Guid userId)
        {
            var user = userRepository.FindById(userId);
            return user.UserRightsEntity;
        }
    }
}