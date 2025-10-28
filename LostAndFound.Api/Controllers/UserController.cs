using LostAndFound.Application.Services.Items;
using LostAndFound.Application.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Api.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost("create-user")]
        public async Task<IActionResult> GetAppUserEmail(CreateUserCommand query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetMyWorkflows([FromQuery] GetUsersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetItemById([FromQuery] GetUserById query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
