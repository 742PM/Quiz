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
        ///     GET api/topics
        /// </remarks>
        /// <response code="200"> Возвращает список тем</response>
        /// <response code="204"> Темы не найдены</response>
        [HttpGet("topics")]
        public ActionResult<IEnumerable<TopicInfoDTO>> GetTopics()
        {
            //ToDo: stab
            return Ok(new[] {new TopicInfoDTO("Complexity", Guid.NewGuid())});
            return Ok(applicationApi.GetTopicsInfo().Select(Mapper.Map<TopicInfoDTO>));
        }

        /// <summary>
        ///     Возвращает всех список уровней в теме.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/levels/0
        /// </remarks>
        /// <response code="200"> Возвращает список сложностей</response>
        /// <response code="204"> Темы не найдены</response>
        [HttpGet("levels/{topicId}")]
        public ActionResult<IEnumerable<LevelInfoDTO>> GetLevels(Guid topicId)
        {
            //ToDo: stab
            return Ok(new[] {new LevelInfoDTO(Guid.NewGuid(), "Complexity")});
            return Ok(applicationApi.GetLevels(topicId).Select(Mapper.Map<LevelInfoDTO>));
        }

        /// <summary>
        ///     Возвращает список уровней в теме доступных юзеру.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/availableLevels/0
        /// </remarks>
        /// <response code="200"> Возвращает список сложностей</response>
        /// <response code="204"> Темы не найдены</response>
        [HttpGet("{userId}/availableLevels/{topicId}")]
        public ActionResult<IEnumerable<LevelInfoDTO>> GetAvailableLevels(Guid userId, Guid topicId)
        {
            //ToDo: stab
            return Ok(new[] {new LevelInfoDTO(Guid.NewGuid(), "Complexity")});

            return Ok(applicationApi.GetAvailableLevels(userId, topicId).Select(Mapper.Map<LevelInfoDTO>));
        }

        /// <summary>
        ///     Возвращает прогресс пользователя по текущему Level.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/currentProgress/0/0
        /// </remarks>
        /// <response code="200"> Отношение решенных задач к общему колличеству задач.</response>
        [HttpGet("{userId}/currentProgress/{topicId}/{levelId}")]
        public ActionResult<double> GetCurrentProgress(Guid userId, Guid topicId, Guid levelId)
        {
            //ToDo: stab
            return Ok(0.5);

            return Ok(applicationApi.GetCurrentProgress(userId, topicId, levelId));
        }

        /// <summary>
        ///     Получить информацию о задании.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/task/1/1
        /// </remarks>
        /// <response code="200"> Возвращает информацию о задании</response>
        [HttpGet("{userId}/task/{topicId}/{levelId}")]
        public ActionResult<TaskInfoDTO> GetTaskInfo(Guid userId, Guid topicId, Guid levelId)
        {
            //ToDo: stab
            return Ok(new TaskInfoDTO("var a = 1;", new[] {"O(1)", "O(n)"}));

            return Ok(Mapper.Map<TaskInfoDTO>(applicationApi.GetTask(userId, topicId, levelId)));
        }

        /// <summary>
        ///     Возвращает информацию о уровне из такого же состояния (тема + уровень).
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/nextTask
        /// </remarks>
        /// <response code="200"> Возвращает информацию уровня</response>
        [HttpGet("{userId}/nextTask")]
        public ActionResult<TaskInfoDTO> GetNextTaskInfo(Guid userId)
        {
            //ToDo: stab
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
        [HttpGet("{userId}/hint")]
        public ActionResult<string> GetHint(Guid userId)
        {
            //ToDo: stab
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
        public ActionResult<bool> SendAnswer([FromBody] string answer, Guid userId)
        {
            //ToDo: stab
            return Ok(true);
            return Ok(applicationApi.CheckAnswer(userId, answer));
        }
    }
}