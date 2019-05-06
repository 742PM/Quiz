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
        /// <response code="404"> Темы не найдены</response>
        [HttpGet("topics")]
        public ActionResult<IEnumerable<TopicInfoDTO>> GetTopics()
        {
            var (_, _, topics) = applicationApi.GetTopicsInfo();
            return Ok(topics.Select(Mapper.Map<TopicInfoDTO>));
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
            var (isSuccess, _, levels, error) = applicationApi.GetLevels(topicId);
            if (isSuccess)
                return Ok(levels.Select(Mapper.Map<LevelInfoDTO>));
            return NotFound(error.Message);
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
            var (isSuccess, _, availableLevels, error) = applicationApi.GetAvailableLevels(userId, topicId);
            if (isSuccess)
                return Ok(availableLevels.Select(Mapper.Map<LevelInfoDTO>));
            return NotFound(error.Message);
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
            var (isSuccess, _, progress, error) = applicationApi.GetCurrentProgress(userId, topicId, levelId);
            if (isSuccess)
                return Ok(progress);
            return NotFound(error.Message);
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
            var (isSuccess, _, task, error) = applicationApi.GetTask(userId, topicId, levelId);
            if (isSuccess)
                return Ok(Mapper.Map<TaskInfoDTO>(task));
            switch (error)
            {
                case ArgumentException _:
                    return NotFound(error.Message);
                case AccessDeniedException _:
                    return Forbid(error.Message);
                default:
                    return InternalServerError(error.Message);
            }
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
            var (isSuccess, _, task, error) = applicationApi.GetNextTask(userId);
            if (isSuccess)
                return Ok(Mapper.Map<TaskInfoDTO>(task));
            if (error is AccessDeniedException)
                return Forbid(error.Message);
            return InternalServerError(error.Message);
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
            var (isSuccess, _, hint, error) = applicationApi.GetHint(userId);
            if (isSuccess)
                return Ok(hint);
            switch (error)
            {
                case AccessDeniedException _:
                    return Forbid(error.Message);
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
        /// <response code="400"> Не удалось получить информацию об ответе</response>
        [HttpPost("{userId}/sendAnswer")]
        public ActionResult<bool> SendAnswer([FromBody] string answer, Guid userId)
        {
            var (isSuccess, _, result, error) = applicationApi.CheckAnswer(userId, answer);
            if (isSuccess)
                return Ok(result);
            if (error is AccessDeniedException)
                return Forbid(error.Message);
            return InternalServerError(error.Message);
        }

        private ObjectResult InternalServerError(object value) =>
            StatusCode(StatusCodes.Status500InternalServerError, value);
    }
}