using LostAndFound.Application.Services.Claims;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Api.Controllers
{
    public class ClaimController : ApiController
    {
        [HttpPost("create-claim")]
        public async Task<IActionResult> CreateClaim(CreateClaimCommand query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("get-claims")]
        public async Task<IActionResult> GetClaims([FromQuery] GetClaimsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("get-claim-by-id")]
        public async Task<IActionResult> GetClaimById([FromQuery] GetClaimByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
