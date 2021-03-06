﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizBotCore.Commands;
using QuizBotCore.Database;
using QuizRequestService;
using QuizWebHookBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizWebHookBot.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly IBotService botService;
        private readonly ServiceManager manager;
        private readonly IUpdateService updateService;

        public UpdateController(IUpdateService updateService, IBotService botService, ServiceManager manager)
        {
            this.updateService = updateService;
            this.botService = botService;
            this.manager = manager;
        }

        // POST api/update
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            Chat chat;
            switch (update.Type)
            {
                case UpdateType.Message:
                    chat = update.Message.Chat;
                    break;
                case UpdateType.CallbackQuery:
                    chat = update.CallbackQuery.Message.Chat;
                    await botService.Client.AnswerCallbackQueryAsync(update.CallbackQuery.Id);
                    break;
                default:
                    return Ok();
//                case UpdateType.ChannelPost:
//                    chat = update.ChannelPost.Chat;
//                    break;
//                case UpdateType.EditedMessage:
//                    chat = update.EditedMessage.Chat;
//                    break;
            }

//            await botService.Client.SendTextMessageAsync(chat.Id, update.Type.ToString());

            var userCommand = updateService.ProcessMessage(update);
            
            await userCommand.ExecuteAsync(chat, botService.Client, manager);
            
            return Ok();
        }
    }
}