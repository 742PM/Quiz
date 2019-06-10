using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using QuizBotCore.Database;

namespace QuizWebHookBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly MessageTextRepository repository;

        public StatusController(MessageTextRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Обновляет набор сообщений из БД с тем же айди, что был раньше
        /// </summary>
        /// <returns>true -- обновление произошло успешно, иначе false</returns>
        [HttpOptions("/reload")] //может быть не опшнс, хз
        public ActionResult ReloadMessages() => repository.UpdateMessages() ? (ActionResult) Ok(): NotFound();

        /// <summary>
        /// Устанавливает набор сообщений с данным айди
        /// </summary>
        /// <param name="messagesId"></param>
        /// <returns></returns>
        [HttpOptions("/set/{messagesId}")] //может быть не опшнс, хз
        public ActionResult<bool> SetMessages(Guid messagesId) => repository.SetMessages(messagesId) ? (ActionResult)Ok() : NotFound();

        [HttpGet]
        public IActionResult Get() => Ok("Bot is running happily");
    }
}