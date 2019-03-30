using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using AutoMapper;
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
        /// <response code="404"> Темы не найдены</response>
        [HttpGet("topics")]
        public ActionResult<IEnumerable<TopicInfoDTO>> GetTopics()
        {
            var (isSuccess, _, topics) = applicationApi.GetTopicsInfo();
            if (isSuccess)
                return Ok(topics.Select(Mapper.Map<TopicInfoDTO>));
            return NotFound("Topics not found");
        }

        /// <summary>
        ///     Возвращает всех список уровней в теме.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/levels
        /// </remarks>
        /// <response code="200"> Возвращает список сложностей</response>
        /// <response code="404"> Уровни не найдены</response>
        [HttpGet("{topicId}/levels")]
        public ActionResult<IEnumerable<LevelInfoDTO>> GetLevels(Guid topicId)
        {
            var (isSuccess, _, levels) = applicationApi.GetLevels(topicId);
            if (isSuccess)
                return Ok(levels.Select(Mapper.Map<LevelInfoDTO>));
            return NotFound("Levels not found");
        }

        /// <summary>
        ///     Возвращает список уровней в теме доступных юзеру.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/0/availableLevels
        /// </remarks>
        /// <response code="200"> Возвращает список сложностей</response>
        /// <response code="404"> Темы не найдены</response>
        [HttpGet("{userId}/{topicId}/availableLevels")]
        public ActionResult<IEnumerable<LevelInfoDTO>> GetAvailableLevels(Guid userId, Guid topicId)
        {
            var (isSuccess, _, availableLevels) = applicationApi.GetAvailableLevels(userId, topicId);
            if (isSuccess)
                return Ok(availableLevels.Select(Mapper.Map<LevelInfoDTO>));
            return NotFound("Available levels not found");
        }

        /// <summary>
        ///     Возвращает прогресс пользователя по текущему Level.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/0/0/currentProgress
        /// </remarks>
        /// <response code="200"> Отношение решенных задач к общему колличеству задач.</response>
        /// <response code="404"> Не удалось получить прогресс пользователя.</response>
        [HttpGet("{userId}/{topicId}/{levelId}/currentProgress")]
        public ActionResult<double> GetCurrentProgress(Guid userId, Guid topicId, Guid levelId)
        {
            var (isSuccess, _, progress) = applicationApi.GetCurrentProgress(userId, topicId, levelId);
            if (isSuccess)
                return Ok(progress);
            return NotFound("Failed, trying to get user progress");
        }

        /// <summary>
        ///     Получить информацию о задании.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/1/1/task
        /// </remarks>
        /// <response code="200"> Возвращает информацию о задании</response>
        /// <response code="404"> Информация о задании не была найдена</response>
        [HttpGet("{userId}/{topicId}/{levelId}/task")]
        public ActionResult<TaskInfoDTO> GetTaskInfo(Guid userId, Guid topicId, Guid levelId)
        {
            var (isSuccess, _, task) = applicationApi.GetTask(userId, topicId, levelId);
            if (isSuccess)
                return Ok(Mapper.Map<TaskInfoDTO>(task));
            return NotFound("Task info not found");
        }

        /// <summary>
        ///     Возвращает информацию о уровне из такого же состояния (тема + уровень).
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/nextTask
        /// </remarks>
        /// <response code="200"> Возвращает информацию следующего уровня</response>
        /// <response code="404"> Информация о следующем уровне не была найдена</response>
        [HttpGet("{userId}/nextTask")]
        public ActionResult<TaskInfoDTO> GetNextTaskInfo(Guid userId)
        {
            var (isSuccess, _, task) = applicationApi.GetNextTask(userId);
            if (isSuccess)
                return Ok(Mapper.Map<TaskInfoDTO>(task));
            return NotFound("Next task info not found");
        }

        /// <summary>
        ///     Выдает подсказку на заданный уровень.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/getHint
        /// </remarks>
        /// <response code="200"> Возвращает подсказку на уровень</response>
        /// <response code="404"> Подсказки закончились</response>
        [HttpGet("{userId}/hint")]
        public ActionResult<string> GetHint(Guid userId)
        {
            var (isSuccess, _, hint) = applicationApi.GetHint(userId);
            if (isSuccess)
                return Ok(hint);
            return NoContent();
        }

        /// <summary>
        ///     Отправить ответ на сервер.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     POST api/0/sendAnswer
        ///     "O(1)"
        /// </remarks>
        /// <response code="200"> Возвращает bool верный ли ответ</response>
        /// <response code="400"> Не удалось получить информацию об ответе</response>
        [HttpPost("{userId}/sendAnswer")]
        public ActionResult<bool> SendAnswer([FromBody] string answer, Guid userId)
        {
            var (isSuccess, _, result) = applicationApi.CheckAnswer(userId, answer);
            if (isSuccess)
                return Ok(result);
            return BadRequest("Error, trying to send an answer");
        }
    }
}