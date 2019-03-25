using System.Collections.Generic;
using Application;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public ActionResult<IEnumerable<string>> GetTopics()
        {
            //stab
            return Ok(new[] {"Циклы", "Строки"});
        }

        /// <summary>
        ///     Возвращает максимальный доступный уровень доступа (сложности) по данной теме
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/1/getTopicAccessLevel
        /// </remarks>
        /// <response code="200"> Уровень доступа по теме</response>
        /// <response code="204"> Уровень доступа по теме не найден, возможно тема не существует</response>
        [HttpGet("{userId}/{topicId}/getTopicAccessLevel")]
        public ActionResult<int> GetMaxAvailableDifficultyOfOfTopic(int userId, int topicId)
        {
            //stab
            if (topicId == 0)
                return Ok(5);

            //должно быть преобразование tocken в guid потом вызов GetAvailableDifficulties

            return NoContent();
        }


        /// <summary>
        ///     Возвращает информацию о уровне.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/1/1/getLevelInfo
        /// </remarks>
        /// <response code="200"> Возвращает информацию уровня</response>
        [HttpGet("{userId}/{topic}/{level}/getLevelInfo")]
        public JsonResult GetLevelInfo(int userId, int topicId, int difficulty)
        {
            //stab
            if (topicId == 1 && difficulty == 1)
                return new JsonResult(new LevelDTO());

            return new JsonResult(null);

            //переписать DTO таска, адаптировать ее под TaskDescription 
            //TaskDescription GetTask(Guid userId, Guid topicId, int difficulty);
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
        public JsonResult GetNextLevelInfo(int userId)
        {
            //stab
            return new JsonResult(new LevelDTO());

            //TaskDescription GetNextTask(Guid userId);
            //анологично GetLevelInfo
        }

        /// <summary>
        ///     Возвращает информацию о следующем уровне из текущго состояние (тема + сложность).
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/1/getSimilarLevelInfo
        /// </remarks>
        /// <response code="200"> Возвращает информацию уровня</response>
        [HttpGet("{userId}/getSimilarLevelInfo")]
        public JsonResult GetSimilarLevelInfo(int userId)
        {
            //stab
            return new JsonResult(new LevelDTO());

            //TaskDescription GetSimilarTask(Guid userId);
            //анологично GetLevelInfo
        }


        /// <summary>
        ///     Возвращает подсказку на заданный уровень.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/0/getHint
        /// </remarks>
        /// <response code="200"> Возвращает подсказку на уровень</response>
        /// <response code="204"> Подсказка не найдена</response>
        [HttpGet("{userId}/getHint")]
        public ActionResult<string> GetHint(int userId)
        {
            //stab
            return Ok("Подсказок больше нет");

            //string GetHint(Guid userId);
        }


        /// <summary>
        ///     Отправить ответ на сервер.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     POST api/0/sendAnswer
        ///     {
        ///     "key": "right answer"
        ///     }
        /// </remarks>
        /// <response code="200"> Возвращает bool верный ли ответ</response>
        [HttpPost("{userId}/sendAnswer")]
        public ActionResult<bool> PostAnswer([FromBody] AnswerDTO value, int userId)
        {
            //stab
            return Ok(true);

            //bool CheckAnswer(Guid userId, string answer);
        }

        [JsonObject]
        public class AnswerDTO
        {
            [JsonProperty("key")] public string Answer { get; set; }
        }

        [JsonObject]
        internal class LevelDTO
        {
            [JsonProperty("name")] public string Name { get; set; } = "Циклы, легкое";

            [JsonProperty("code")] public string Code { get; set; } = "for (var i =0; i < n; i++)\n    a = i;";

            [JsonProperty("description")]
            public string LevelDescription { get; set; } = "Посчитайте сложность вычисления n операций";

            [JsonProperty("answers")]
            public IEnumerable<string> Answers { get; set; } = new[] {"O(n)", "O(1)", "O(n*log(n)), O(n^2)"};
        }
    }
}