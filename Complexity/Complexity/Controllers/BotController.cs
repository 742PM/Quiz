using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Complexity.Controllers
{
    [Route("api")]
    [ApiController]
    public class BotController : ControllerBase
    {
        // GET api/getTopics
        [HttpGet("getTopics")]
        public ActionResult<IEnumerable<string>> GetTopics()
        {
            return Ok(new[] { "Циклы", "Строки" });
        }

        // GET api/0/getPoints
        [HttpGet("{id}/getPoints")]
        public ActionResult<int> GetPoints(int id)
        {
            return Ok(100);
        }
        
        // GET api/cycles/getLevels
        [HttpGet("{topic}/getLevels")]
        public ActionResult<IEnumerable<string>> GetLevelsOfTopic(string topic)
        {
            if (topic == "cycles")
                return Ok(new[] { "Легко", "Сложно" });

            return NoContent();
        }
        
        // GET api/cycles/getAvailableLevels
        [HttpGet("{topic}/getAvailableLevels")]
        public ActionResult<IEnumerable<string>> GetAvailableLevelsOfTopic(string topic)
        {
            if (topic == "cycles")
                return Ok(new[] { "Легко"});

            return NoContent();
        }
        
        // GET api/cycles/easy/getHint
        [HttpGet("{topic}/{level}/getHint")]
        public ActionResult<string> GetHint(string topic, string level)
        {
            if (topic == "cycles" && level == "easy")
                return Ok("Подсказок больше нет");

            return NoContent();
        }
        

        // POST api/cycles/easy/sendAnswer
        [HttpPost("{topic}/{level}/sendAnswer")]
        public ActionResult<bool> PostAnswer([FromBody]AnswerDTO value, [FromRoute]string topic, [FromRoute]string level)
        {
            //TODO 415
            if (topic == "cycles" && level == "easy" && value.Answer == "right answer")
                return Ok(true);
            if (topic == "cycles" && level == "easy" && value.Answer == "wrong answer")
                return Ok(false);

            return BadRequest(new[] {value.Answer, topic, level});
        }
        
        // GET api/cycles/easy/getLevelInfo
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
            [JsonProperty("key")]
            public string Answer { get; set; }
        }

        [JsonObject]
        internal class LevelDTO
        {
            [JsonProperty("name")]
            public string Name { get; set; } = "Циклы, легкое";
            
            [JsonProperty("code")]            
            public string Code { get; set; } = "for (var i =0; i < n; i++)\n    a = i;";
            
            [JsonProperty("description")]
            public string LevelDescription { get; set; } = "Посчитайте сложность вычисления n операций";
            
            [JsonProperty("answers")]
            public IEnumerable<string> Answers { get; set; } = new[] {"O(n)", "O(1)", "O(n*log(n)), O(n^2)"};
        }
    }
}
