using LostAndFound.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using LostAndFound.Application.Services.Claims;
using LostAndFound.Infrastructure.Identity; // Your custom user
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

//[Authorize]
[Route("api/[controller]")]
public class ClaimController : ControllerBase
{
    private readonly ISender _mediator;

    public ClaimController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("create-claim")]
    public async Task<IActionResult> CreateClaim(CreateClaimCommand query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("get-claims")]
    public async Task<IActionResult> GetClaims([FromQuery] GetClaimsQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("get-claim-by-id")]
    public async Task<IActionResult> GetClaimById([FromQuery] GetClaimByIdQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}
