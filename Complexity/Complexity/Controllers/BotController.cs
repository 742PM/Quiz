using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Complexity.Controllers
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
        ///     GET api/getTopics
        /// </remarks>
        /// <response code="200"> Возвращает список тем</response>
        /// <response code="204"> Темы не найдены</response>
        [HttpGet("getTopics")]
        public ActionResult<IEnumerable<TopicInfoDTO>> GetTopics()
        {
            //stab
            return Ok(new[] {new TopicInfoDTO("Complexity", Guid.NewGuid())});
            return Ok(applicationApi.GetTopicsInfo().Select(Mapper.Map<TopicInfoDTO>));
        }

        /// <summary>
        ///     Возвращает всех список уровней в теме.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/getLevels/0
        /// </remarks>
        /// <response code="200"> Возвращает список сложностей</response>
        /// <response code="204"> Темы не найдены</response>
        [HttpGet("getLevels/{topicId}")]
        public ActionResult<IEnumerable<LevelInfoDTO>> GetLevels(Guid topicId)
        {
            //stab
            return Ok(new[] {new LevelInfoDTO(Guid.NewGuid(), "Complexity")});
            return Ok(applicationApi.GetLevels(topicId).Select(Mapper.Map<LevelInfoDTO>));
        }

        /// <summary>
        ///     Возвращает список уровней в теме доступных юзеру.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/getAvailableLevels/0
        /// </remarks>
        /// <response code="200"> Возвращает список сложностей</response>
        /// <response code="204"> Темы не найдены</response>
        [HttpGet("{userId}/getAvailableLevels/{topicId}")]
        public ActionResult<IEnumerable<TopicInfoDTO>> GetAvailableLevels(Guid userId, Guid topicId)
        {
            //stab
            return Ok(new[] {new LevelInfoDTO(Guid.NewGuid(), "Complexity")});

            return Ok(applicationApi.GetAvailableLevels(userId, topicId).Select(Mapper.Map<LevelInfoDTO>));
        }

        /// <summary>
        ///     Возвращает прогресс пользователя по текущему Level.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/getCurrentProgress/0/0
        /// </remarks>
        /// <response code="200"> Отношение решенных задач к общему колличеству задач.</response>
        [HttpGet("{userId}/getCurrentProgress/{topicId}/{levelId}")]
        public ActionResult<double> GetAvailableLevels(Guid userId, Guid topicId, Guid levelId)
        {
            //stab
            return Ok(0.5);

            return Ok(applicationApi.GetCurrentProgress(userId, topicId, levelId));
        }

        /// <summary>
        ///     Возвращает информацию о уровне.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/1/1/getLevelInfo
        /// </remarks>
        /// <response code="200"> Возвращает информацию о уровне</response>
        [HttpGet("{userId}/{topicId}/{levelId}/getLevelInfo")]
        public ActionResult<TaskInfoDTO> GetLevelInfo(Guid userId, Guid topicId, Guid levelId)
        {
            //stab
            return Ok(new TaskInfoDTO("var a = 1;", new[] {"O(1)", "O(n)"}));

            return Ok(Mapper.Map<TaskInfoDTO>(applicationApi.GetTask(userId, topicId, levelId)));
        }

        /// <summary>
        ///     Возвращает информацию о уровне из такого же состояния (тема + сложность).
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/getNextLevelInfo
        /// </remarks>
        /// <response code="200"> Возвращает информацию уровня</response>
        [HttpGet("{userId}/getNextLevelInfo")]
        public ActionResult<TaskInfoDTO> GetNextLevelInfo(Guid userId)
        {
            //stab
            return Ok(new TaskInfoDTO("var a = 1;", new[] {"O(1)", "O(n)"}));

            return Ok(Mapper.Map<TaskInfoDTO>(applicationApi.GetNextTask(userId)));
        }

        /// <summary>
        ///     Выдает подсказку на заданный уровень.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/getHint
        /// </remarks>
        /// <response code="200"> Возвращает подсказку на уровень</response>
        /// <response code="204"> Подсказка не найдена</response>
        [HttpGet("{userId}/getHint")]
        public ActionResult<string> GetHint(Guid userId)
        {
            //stab
            return Ok("Подсказок больше нет");
            return Ok(applicationApi.GetHint(userId));
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
        [HttpPost("{userId}/sendAnswer")]
        public ActionResult<bool> CheckAnswer([FromBody] string answer, Guid userId)
        {
            //stab
            return Ok(true);
            return Ok(applicationApi.CheckAnswer(userId, answer));
        }
    }
}