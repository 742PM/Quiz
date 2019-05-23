using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuizRequestExtendedService;
using QuizRequestExtendedService.DTO;

namespace QuizLevelManager.Controllers
{
    [Route("proxy")]
    [ApiController]
    public class ProxyController : Controller
    {
        private readonly IQuizServiceExtended service;

        public ProxyController(IQuizServiceExtended service)
        {
            this.service = service;
        }

        [HttpGet("topics")]
        public ActionResult<IEnumerable<TopicDTO>> GetTopics()
        {
            return Ok(service.GetTopics());
        }

        [HttpGet("{topicId}/levels")]
        public ActionResult<IEnumerable<LevelDTO>> GetLevels(Guid topicId)
        {
            return Ok(service.GetLevels(topicId));
        }

        [HttpGet("{topicId}/{levelId}/templateGenerators")]
        public ActionResult<IEnumerable<AdminTemplateGeneratorDTO>> GetTemplateGenerators(Guid topicId, Guid levelId)
        {
            return Ok(service.GetTemplateGenerators(topicId, levelId));
        }

        [HttpPost("topic")]
        public ActionResult<Guid> AddEmptyTopic([FromBody] EmptyTopicDTO topic)
        {
            return Ok(service.AddEmptyTopic(topic));
        }

        [HttpDelete("topic/{topicId}")]
        public ActionResult<Guid> DeleteTopic(Guid topicId)
        {
            service.DeleteTopic(topicId);
            return Ok();
        }

        [HttpPost("{topicId}/level")]
        public ActionResult<Guid> AddEmptyLevel(Guid topicId, [FromBody] EmptyLevelDTO level)
        {
            return Ok(service.AddEmptyLevel(topicId, level));
        }

        [HttpDelete("{topicId}/level/{levelId}")]
        public ActionResult<Guid> DeleteLevel(Guid topicId, Guid levelId)
        {
            service.DeleteLevel(topicId, levelId);
            return Ok();
        }

        [HttpPost("{topicId}/{levelId}/templategenerator")]
        public ActionResult<Guid> AddTemplateGenerator(Guid topicId, Guid levelId, [FromBody] TemplateGeneratorDTO generator)
        {
            return Ok(service.AddEmptyGenerator(topicId, levelId, generator));
        }

        [HttpDelete("{topicId}/{levelId}/generator/{generatorId}")]
        public ActionResult<Guid> DeleteGenerator(Guid topicId, Guid levelId, Guid generatorId)
        {
            service.DeleteTemplateGenerator(topicId, levelId, generatorId);
            return Ok();
        }
        
        
        [HttpPost("tasktorender")]
        public ActionResult<TaskDTO> RenderTask([FromBody] TemplateGeneratorForRenderDTO templateGenerator)
        {
            return Ok(service.RenderTask(templateGenerator));
        }
    }
}