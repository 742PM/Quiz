using System;
using System.Threading.Tasks;
using Application.QuizService;
using Application.Repositories;
using Application.Selectors;
using DataBase;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using QuizBotCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace IntergratioTests
{
    [TestFixture]
    public class QuizServiceTest
    {
        private TelegramBotClient telegramBotClient;
        private const string testChannel = "@QuibbleBotTestService";

        [SetUp]
        public void SetUp()
        {
            telegramBotClient = new TelegramBotClient("840366370:AAEROBZsf6wxPg_5D8eVoF9ibJ4DyFnqiuQ");
        }

        public async Task<Message> SendTelegramMessage(string message)
        {
            return await telegramBotClient.SendTextMessageAsync(testChannel, message);
        }

        [Test]
        public void DoSomething_WhenSomething()
        {
            var answer = SendTelegramMessage("hello");
            answer.Result.Text.Should().Be(DialogMessages.Welcome);
        }
        
    }
}