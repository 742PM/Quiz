using System;
using System.Collections.Generic;
using System.Linq;
using Application.Exceptions;
using Application.QuizService;
using AutoMapper;
using ComplexityWebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplexityWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class QuizServiceController : ControllerBase
    {
        private readonly IQuizService applicationApi;

        public QuizServiceController(IQuizService applicationApi)
        {
            this.applicationApi = applicationApi;
        }

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
            var topics = applicationApi.GetTopicsInfo().Value;
            return Ok(topics.Select(Mapper.Map<TopicInfoDTO>));
        }

        /// <summary>
        ///     Возвращает всех список уровней в теме.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/levels
        /// </remarks>
        /// <response code="200"> Возвращает список уровней в теме</response>
        /// <response code="404"> Id темы не найден</response>
        [HttpGet("{topicId}/levels")]
        public ActionResult<IEnumerable<LevelInfoDTO>> GetLevels(Guid topicId)
        {
            var (isSuccess, _, levels, error) = applicationApi.GetLevels(topicId);
            if (isSuccess)
                return Ok(levels.Select(Mapper.Map<LevelInfoDTO>));
            return NotFound(error.Message);
        }

        /// <summary>
        ///     Возвращает список уровней в теме доступных пользователю.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/0/availableLevels
        /// </remarks>
        /// <response code="200"> Возвращает список доступных уровней</response>
        /// <response code="404"> Id темы не найден</response>
        [HttpGet("{userId}/{topicId}/availableLevels")]
        public ActionResult<IEnumerable<LevelInfoDTO>> GetAvailableLevels(Guid userId, Guid topicId)
        {
            var (isSuccess, _, availableLevels, error) = applicationApi.GetAvailableLevels(userId, topicId);
            if (isSuccess)
                return Ok(availableLevels.Select(Mapper.Map<LevelInfoDTO>));
            return NotFound(error.Message);
        }

        /// <summary>
        ///     Возвращает прогресс пользователя.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/0/0/currentProgress
        /// </remarks>
        /// <response code="200"> Возвращает прогресс пользователя</response>
        /// <response code="404"> Id темы или уровня не найдены</response>
        [HttpGet("{userId}/{topicId}/{levelId}/currentProgress")]
        public ActionResult<LevelProgressInfoDTO> GetCurrentProgress(Guid userId, Guid topicId, Guid levelId)
        {
            var (isSuccess, _, progress, error) = applicationApi.GetCurrentProgress(userId, topicId, levelId);
            if (isSuccess)
                return Ok(Mapper.Map<LevelProgressInfoDTO>(progress));
            return NotFound(error.Message);
        }

        /// <summary>
        ///     Получить задачу.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/1/1/task
        /// </remarks>
        /// <response code="200"> Возвращает задачу</response>
        /// <response code="404"> Id темы или уровня не найдены</response>
        [HttpGet("{userId}/{topicId}/{levelId}/task")]
        public ActionResult<TaskInfoDTO> GetTaskInfo(Guid userId, Guid topicId, Guid levelId)
        {
            var (isSuccess, _, task, error) = applicationApi.GetTask(userId, topicId, levelId);
            if (isSuccess)
                return Ok(Mapper.Map<TaskInfoDTO>(task));
            switch (error)
            {
                case ArgumentException _:
                    return NotFound(error.Message);
                case AccessDeniedException _:
                    return Forbid();
                default:
                    return InternalServerError(error.Message);
            }
        }

        /// <summary>
        ///     Возвращает следующую задачу из текущих темы и уровня пользователя.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/nextTask
        /// </remarks>
        /// <response code="200"> Возвращает следующую задачу</response>
        /// <response code="403"> Данная операция не доступна пользователю</response>
        [HttpGet("{userId}/nextTask")]
        public ActionResult<TaskInfoDTO> GetNextTaskInfo(Guid userId)
        {
            var (isSuccess, _, task, error) = applicationApi.GetNextTask(userId);
            if (isSuccess)
                return Ok(Mapper.Map<TaskInfoDTO>(task));
            if (error is AccessDeniedException)
                return Forbid();
            return InternalServerError(error.Message);
        }

        /// <summary>
        ///     Возвращает подсказку на текущую задачу.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/getHint
        /// </remarks>
        /// <response code="200"> Возвращает подсказку на текущую задачу</response>
        /// <response code="204"> Подсказки закончились</response>
        /// <response code="403"> Данная операция не доступна пользователю</response>
        [HttpGet("{userId}/hint")]
        public ActionResult<HintInfoDTO> GetHint(Guid userId)
        {
            var (isSuccess, _, hint, error) = applicationApi.GetHint(userId);
            if (isSuccess)
                return Ok(Mapper.Map<HintInfoDTO>(hint));
            switch (error)
            {
                case AccessDeniedException _:
                    return Forbid();
                case OutOfHintsException _:
                    return NoContent();
                default:
                    return InternalServerError(error.Message);
            }
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
        /// <response code="403"> Данная операция не доступна пользователю</response>
        [HttpPost("{userId}/sendAnswer")]
        public ActionResult<bool> SendAnswer([FromBody] string answer, Guid userId)
        {
            var (isSuccess, _, result, error) = applicationApi.CheckAnswer(userId, answer);
            if (isSuccess)
                return Ok(result);
            if (error is AccessDeniedException)
                return Forbid();
            return InternalServerError(error.Message);
        }

        private ObjectResult InternalServerError(object value) =>
            StatusCode(StatusCodes.Status500InternalServerError, value);
    }
}