using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using Application.Exceptions;
using AutoMapper;
using ComplexityWebApi.DTO;
using Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;

namespace ComplexityWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class QuizServiceController : ControllerBase
    {
        private IQuizService applicationApi;

        /// <summary>
        ///     Возвращает список всех тем.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/topics
        /// </remarks>
        /// <response code="200"> Возвращает список тем</response>
        [HttpGet("topics")]
        public ActionResult<IEnumerable<TopicInfoDTO>> GetTopics()
        {
            return applicationApi.GetTopicsInfo()
                                 .OnSuccess(ts => Ok(ts.Select(Mapper.Map<TopicInfoDTO>)))
                                 .Value;
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
            return applicationApi.GetLevels(topicId)
                          .Then(ls => (ls.Select(Mapper.Map<LevelInfoDTO>)))
                          .Then(Ok)
                          .OnFailure(err => NotFound(err is ArgumentException ? $"Invalid TopicId {topicId}" : $"Levels not found: {err.Message}"))
                          .Value;
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
            return applicationApi.GetAvailableLevels(userId, topicId)
                                 .Then(al => al.Select(Mapper.Map<LevelInfoDTO>))
                                 .Then(Ok)
                                 .OnFailure(err => NotFound(err is ArgumentException ? $"Invalid UserId or TopicId:{userId}\n{topicId}" : $"Levels not found: {err.Message}"))
                                 .Value;
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
            return applicationApi.GetCurrentProgress(userId, topicId, levelId)
                                 .Then(p=>Ok(p))
                                 .OnFailure(err => NotFound(err is ArgumentException ? $"Invalid UserId or TopicId or LevelId:{userId}\n{topicId}\n{levelId}" : $"Progress not found: {err.Message}"))
                                 .Value;
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
        /// <response code="403"> Отказано в доступе к заданию данному пользователю</response>
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
