using LostAndFound.Application.Services.Disputes;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Api.Controllers
{
    public class DisputeController : ApiController
    {
        [HttpPost("create-dispute")]
        public async Task<IActionResult> CreateDispute(CreateDisputeCommand query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("get-disputes")]
        public async Task<IActionResult> GetDisputes([FromQuery] GetDisputesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("get-dispute-by-id")]
        public async Task<IActionResult> GetDisputeById([FromQuery] GetDisputeByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
