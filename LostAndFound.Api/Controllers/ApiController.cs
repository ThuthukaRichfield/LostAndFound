using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Api.Controllers
{
    //[Authorize]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        //[HttpDelete("{id}")]
        //public virtual async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    // The api controller name is also the entity type  
        //    var entityType = ControllerContext.ActionDescriptor.ControllerName;

        //    return Ok(await Mediator.Send(new DeleteCommand()
        //    {
        //        EntityType = entityType,
        //        Id = id
        //    })); ;
        //}
    }
}
