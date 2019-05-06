using System;
using System.Collections.Generic;
using System.Linq;
using Application;
using AutoMapper;
using ComplexityWebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ComplexityWebApi.Controllers
{
    [Route("service")]
    [ApiController]
    public class TaskServiceController : ControllerBase
    {
        private readonly ITaskService applicationApi;

        [HttpGet("topics")]
        public ActionResult<IEnumerable<TopicInfoDTO>> GetTopics()
        {
            var topics = applicationApi.GetAllTopics();
            return Ok(topics.Select(Mapper.Map<TopicInfoDTO>));
        }

        [HttpPost("addTopic/{name}")]
        public ActionResult<Guid> AddEmptyTopic(string name, [FromBody] string description)
        {
            var topicGuid = applicationApi.AddEmptyTopic(name, description);
            return Ok(topicGuid);
        }

        [HttpDelete("deleteTopic/{topicId}")]
        public ActionResult DeleteTopic(Guid topicId)
        {
            var (_, _) = applicationApi.DeleteTopic(topicId);
            return Ok();
        }

        [HttpPost("addLevel/{topicId}")]
        public ActionResult<Guid> AddLevel(Guid topicId, [FromBody] DataBaseLevelDTO level)
        {
            var (levelGuid, _) = applicationApi.AddEmptyLevel(topicId, level.Description, level.PreviousLevels, level.NextLevels);
            return Ok(levelGuid);
        }

        [HttpDelete("deleteLevel/{topicId}/{levelId}")]
        public ActionResult DeleteLevel(Guid topicId, Guid levelId)
        {
            var (_, _) = applicationApi.DeleteLevel(topicId, levelId);
            return Ok();
        }

        [HttpPost("addTemplateGenerator/{topicId}/{levelId}")]
        public ActionResult<Guid> AddTemplateGenerator(Guid topicId, Guid levelId, [FromBody] DataBaseTemplateGeneratorWithStreakDTO templateGenerator)
        {
            var (generatorGuid, _) = applicationApi.AddTemplateGenerator(topicId, levelId, templateGenerator.Template, templateGenerator.PossibleAnswers,
                templateGenerator.RightAnswer, templateGenerator.Hints, templateGenerator.Streak);
            return Ok(generatorGuid);
        }

        [HttpDelete("deleteGenerator/{topicId}/{levelId}/{generatorId}")]
        public ActionResult DeleteGenerator(Guid topicId, Guid levelId, Guid generatorId)
        {
            var (_, _) = applicationApi.DeleteGenerator(topicId, levelId, generatorId);
            return Ok();
        }

        [HttpPost("renderTemplateGenerator")]
        public ActionResult RenderTemplateGenerator([FromBody] DataBaseTemplateGeneratorDTO templateGenerator)
        {
            var task = applicationApi.RenderTask(templateGenerator.Template, templateGenerator.PossibleAnswers,
                templateGenerator.RightAnswer, templateGenerator.Hints);

            return Ok(task);
        }
    }
}