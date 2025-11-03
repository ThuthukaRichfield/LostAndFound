using LostAndFound.Application.Services.Items;
using LostAndFound.Application.Services.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ISender _mediator;

        public ItemController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("report-lost-item")]
        public async Task<IActionResult> ReportLostItem(ReportLostItemCommand query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("report-found-item")]
        public async Task<IActionResult> ReportFoundItem(ReportFoundItemCommand query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("get-lost-items")]
        public async Task<IActionResult> GetLostItems([FromQuery] GetItemsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("get-item-by-id")]
        public async Task<IActionResult> GetItemById([FromQuery] GetItemByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
