using System;
using System.Collections.Generic;
using Application;
using ComplexityWebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ComplexityWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private IApplicationApi applicationApi;

        /// <summary>
        ///     Возвращает список всех тем.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/topics
        /// </remarks>
        /// <response code="200"> Возвращает список тем</response>
        /// <response code="204"> Темы не найдены</response>
        [HttpGet("topics")]
        public ActionResult<IEnumerable<TopicInfoDTO>> GetTopics() =>
            Ok(new[] {new TopicInfoDTO("Complexity", Guid.NewGuid())});

        /// <summary>
        ///     Возвращает всех список уровней в теме.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/levels
        /// </remarks>
        /// <response code="200"> Возвращает список сложностей</response>
        /// <response code="204"> Темы не найдены</response>
        [HttpGet("{topicId}/levels")]
        public ActionResult<IEnumerable<LevelInfoDTO>> GetLevels(Guid topicId) =>
            Ok(new[] {new LevelInfoDTO(Guid.NewGuid(), "Complexity")});

        /// <summary>
        ///     Возвращает список уровней в теме доступных юзеру.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/0/availableLevels
        /// </remarks>
        /// <response code="200"> Возвращает список сложностей</response>
        /// <response code="204"> Темы не найдены</response>
        [HttpGet("{userId}/{topicId}/availableLevels")]
        public ActionResult<IEnumerable<LevelInfoDTO>> GetAvailableLevels(Guid userId, Guid topicId) =>
            Ok(new[] {new LevelInfoDTO(Guid.NewGuid(), "Complexity")});

        /// <summary>
        ///     Возвращает прогресс пользователя по текущему Level.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/0/0/currentProgress
        /// </remarks>
        /// <response code="200"> Отношение решенных задач к общему колличеству задач.</response>
        [HttpGet("{userId}/{topicId}/{levelId}/currentProgress")]
        public ActionResult<double> GetCurrentProgress(Guid userId, Guid topicId, Guid levelId) => Ok(0.5);

        /// <summary>
        ///     Получить информацию о задании.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/1/1/task
        /// </remarks>
        /// <response code="200"> Возвращает информацию о задании</response>
        [HttpGet("{userId}/{topicId}/{levelId}/task")]
        public ActionResult<TaskInfoDTO> GetTaskInfo(Guid userId, Guid topicId, Guid levelId) =>
            Ok(new TaskInfoDTO("var a = 1;", new[] {"O(1)", "O(n)"}));

        /// <summary>
        ///     Возвращает информацию о уровне из такого же состояния (тема + уровень).
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/nextTask
        /// </remarks>
        /// <response code="200"> Возвращает информацию уровня</response>
        [HttpGet("{userId}/nextTask")]
        public ActionResult<TaskInfoDTO> GetNextTaskInfo(Guid userId) =>
            Ok(new TaskInfoDTO("var a = 1;", new[] {"O(1)", "O(n)"}));

        /// <summary>
        ///     Выдает подсказку на заданный уровень.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/getHint
        /// </remarks>
        /// <response code="200"> Возвращает подсказку на уровень</response>
        /// <response code="204"> Подсказка не найдена</response>
        [HttpGet("{userId}/hint")]
        public ActionResult<string> GetHint(Guid userId) => Ok("Подсказок больше нет");

        /// <summary>
        ///     Отправить ответ на сервер.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     POST api/0/sendAnswer
        ///     "O(1)"
        /// </remarks>
        /// <response code="200"> Возвращает bool верный ли ответ</response>
        [HttpPost("{userId}/sendAnswer")]
        public ActionResult<bool> SendAnswer([FromBody] string answer, Guid userId) => Ok(true);
    }
}
