using System;
using Microsoft.AspNetCore.Mvc;

namespace ComplexityWebApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class EditorsController : ControllerBase
    {
        [HttpPost("login")]
        public ActionResult<Guid> Login([FromBody] EditorDTO editor)
        {
            return Ok();
        }

        [HttpGet("rights")]
        public ActionResult<Guid> GetRights(string username)
        {
            //ToDo remove stab
            return Ok(new[]
            {
                "can_edit_levels",
                "can_edit_topics",
                "can_edit_generators",
                "can_render_tasks",
                "can_get_levels_tasks_generators"
            });
        }
    }
}