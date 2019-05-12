using System;
using Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ComplexityWebApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class EditorsController : ControllerBase
    {
        private readonly IUserRepository usersRepository;

        public EditorsController(IUserRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [HttpPost("login")]
        public ActionResult<Guid> Login([FromBody] EditorDTO editor)
        {
            return Ok();
        }

        [HttpGet("rights")]
        public ActionResult<string[]> GetRights(Guid userId)
        {
            var user = usersRepository.FindById(userId);
            var rights = user.UserRightsEntity.GetRights();
            
            return Ok(rights);
        }
    }
}