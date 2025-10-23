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
    }
}
