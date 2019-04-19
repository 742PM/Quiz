using System;
using Microsoft.AspNetCore.Mvc;

namespace ComplexityWebApi.Controllers
{
    [Obsolete]
    [Route("users")]
    [ApiController]
    public class UserDatabaseController : ControllerBase
    {
        /// <summary>
        ///     Получить userId из базы данных юзеров.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/userIdByTelegramToken/0
        /// </remarks>
        /// <response code="200"> Возвращает userId</response>
        /// <response code="204"> Юзер с таким токеном еще не существует</response>
        [HttpGet("userIdByTelegramToken/{telegramToken}")]
        public ActionResult<Guid> GetUserIdByTelegramToken(string telegramToken) => NoContent();

        /// <summary>
        ///     Добавить userId в базу данных юзеров.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET api/addUserByTelegram/0
        /// </remarks>
        /// <response code="202"> Юзер был добавлен в базу данных</response>
        [HttpPost("addUserByTelegram")]
        public ActionResult AddUserIdToDatabase([FromBody] string telegramToken) => Accepted();
    }
}
