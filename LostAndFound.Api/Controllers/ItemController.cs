using LostAndFound.Application.Services.Items;
using LostAndFound.Application.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Api.Controllers
{
    public class ItemController : ApiController
    {
        [HttpPost("report-lost-item")]
        public async Task<IActionResult> ReportLostItem(ReportLostItemCommand query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("report-found-item")]
        public async Task<IActionResult> ReportFoundItem(ReportFoundItemCommand query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("get-lost-items")]
        public async Task<IActionResult> GetLostItems([FromQuery] GetItemsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("get-item-by-id")]
        public async Task<IActionResult> GetItemById([FromQuery] GetItemByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
