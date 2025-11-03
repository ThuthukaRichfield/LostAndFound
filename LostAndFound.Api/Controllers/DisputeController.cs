using LostAndFound.Application.Services.Disputes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class DisputeController : ControllerBase
    {
        private readonly ISender _mediator;

        public DisputeController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    
        [HttpPost("create-dispute")]
        public async Task<IActionResult> CreateDispute(CreateDisputeCommand query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("get-disputes")]
        public async Task<IActionResult> GetDisputes([FromQuery] GetDisputesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("get-dispute-by-id")]
        public async Task<IActionResult> GetDisputeById([FromQuery] GetDisputeByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
