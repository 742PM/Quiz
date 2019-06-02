using System.Collections.Generic;
using Application.TaskService;
using Domain.Entities.TaskGenerators;
using Microsoft.AspNetCore.Mvc;
using QuizWebApp.Services.TaskService.DTO;

namespace QuizWebApp.Services.TaskService
{
    [Route("templates")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITaskService applicationApi;

        public TemplateController(ITaskService applicationApi)
        {
            this.applicationApi = applicationApi;
        }

        /// <summary>
        ///     Рендерит и возвращает задачу по шаблону из запроса
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     POST templates/renderTask
        ///     {
        ///        "question": "Оцените временную сложность алгоритма",
        ///        "text": "for (int i = {{from1}}; i &lt; {{to1}}; i += {{iter1}})\r\nc++\r\n",
        ///        "possibleAnswers": ["Θ(1)", "Θ(log({{to1}}))", "Θ({{to1}})"],
        ///        "answer": "Θ({{to1}})",
        ///        "hints": []
        ///     }
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает отрендеренную задачу</response>
        [HttpPost("renderTask")]
        public ActionResult RenderTask([FromBody] TemplateGeneratorForRenderDTO templateGenerator)
        {
            var task = applicationApi.RenderTask(
                templateGenerator.Text,
                templateGenerator.PossibleAnswers,
                templateGenerator.Answer,
                templateGenerator.Hints,
                templateGenerator.Question);

            return Ok(task);
        }

        /// <summary>
        ///     Показывает набор существующих по умолчанию подстановок и их значений
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     <code>
        ///     GET templates/substitutions/examples
        ///     </code>
        /// </remarks>
        /// <response code="200"> Возвращает список пар ключ-значение</response>
        [HttpGet("substitutions/examples")]
        public ActionResult<List<TemplateLanguage.SubstitutionData>> GetSubstitutions()
        {
            return Ok(TemplateLanguage.GetValuesExample());
        }
    }
}