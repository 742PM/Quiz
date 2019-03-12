using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Complexity.Controllers
{
    [Route("api")]
    [ApiController]
    public class BotController : ControllerBase
    {
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
            return Ok(new[] {"Циклы", "Строки"});
        }

        // GET api/0/getPoints
        /// <summary>
        ///     Возвращает колличество очков пользователя.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/getPoints
        /// </remarks>
        /// <response code="200"> Возвращает колличество очков пользователя</response>
        [HttpGet("{id}/getPoints")]
        public ActionResult<int> GetPoints(int id)
        {
            return Ok(100);
        }

        /// <summary>
        ///     Возвращает список всех уровней.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/cycles/getLevels
        /// </remarks>
        /// <response code="200"> Возвращает список уровней</response>
        /// <response code="204"> Уровни не найдены</response>
        [HttpGet("{topic}/getLevels")]
        public ActionResult<IEnumerable<string>> GetLevelsOfTopic(string topic)
        {
            if (topic == "cycles")
                return Ok(new[] {"Легко", "Сложно"});

            return NoContent();
        }

        /// <summary>
        ///     Возвращает список доступных уровней.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/cycles/getAvailableLevels
        /// </remarks>
        /// <response code="200"> Возвращает список доступных уровней</response>
        /// <response code="204"> Уровни не найдены</response>
        [HttpGet("{topic}/getAvailableLevels")]
        public ActionResult<IEnumerable<string>> GetAvailableLevelsOfTopic(string topic)
        {
            if (topic == "cycles")
                return Ok(new[] {"Легко"});

            return NoContent();
        }

        /// <summary>
        ///     Возвращает подсказку на заданный уровень.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/cycles/easy/getHint
        /// </remarks>
        /// <response code="200"> Возвращает подсказку на уровень</response>
        /// <response code="204"> Подсказка не найдена</response>
        [HttpGet("{topic}/{level}/getHint")]
        public ActionResult<string> GetHint(string topic, string level)
        {
            if (topic == "cycles" && level == "easy")
                return Ok("Подсказок больше нет");

            return NoContent();
        }


        /// <summary>
        ///     Отправить ответ на сервер.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     POST api/cycles/easy/sendAnswer
        ///     {
        ///     "key": "right answer"
        ///     }
        /// </remarks>
        /// <response code="200"> Возвращает bool верный ли ответ</response>
        [HttpPost("{topic}/{level}/sendAnswer")]
        public ActionResult<bool> PostAnswer([FromBody] AnswerDTO value, [FromRoute] string topic,
            [FromRoute] string level)
        {
            //TODO 415
            if (topic == "cycles" && level == "easy" && value.Answer == "right answer")
                return Ok(true);
            if (topic == "cycles" && level == "easy" && value.Answer == "wrong answer")
                return Ok(false);

            return BadRequest(new[] {value.Answer, topic, level});
        }

        /// <summary>
        ///     Возвращает информацию о уровне.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/cycles/easy/getLevelInfo
        /// </remarks>
        /// <response code="200"> Возвращает информацию уровня</response>
        [HttpGet("{topic}/{level}/getLevelInfo")]
        public JsonResult GetLevelInfo(string topic, string level)
        {
            if (topic == "cycles" && level == "easy")
                return new JsonResult(new LevelDTO());

            return new JsonResult(null);
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