using LostAndFound.Application.Services.Items;
using LostAndFound.Application.Services.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ISender _mediator;

        public UserController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    
        [HttpPost("create-user")]
        public async Task<IActionResult> GetAppUserEmail(CreateUserCommand query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetMyWorkflows([FromQuery] GetUsersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetItemById([FromQuery] GetUserById query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
