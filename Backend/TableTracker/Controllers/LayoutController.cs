using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TableTracker.Application.CQRS.Layout.Commands.AddLayout;
using TableTracker.Application.CQRS.Layout.Commands.DeleteLayout;
using TableTracker.Application.CQRS.Layout.Commands.UpdateLayout;
using TableTracker.Application.CQRS.Layout.Queries.FindLayoutById;
using TableTracker.Application.CQRS.Layout.Queries.FindLayoutByRestaurant;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Helpers;

namespace TableTracker.Controllers
{
    [ApiController]
    [Route("api/layouts")]
    public class LayoutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LayoutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindLayoutById([FromRoute] long id)
        {
            var response = await _mediator.Send(new FindLayoutByIdQuery(id));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddLayout([FromBody] LayoutDTO layoutDTO)
        {
            var response = await _mediator.Send(new AddLayoutCommand(layoutDTO));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateLayout([FromBody] LayoutDTO layoutDTO)
        {
            var response = await _mediator.Send(new UpdateLayoutCommand(layoutDTO));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLayout([FromRoute] long id)
        {
            var response = await _mediator.Send(new DeleteLayoutCommand(id));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpGet("restaurant")]
        public async Task<IActionResult> FindLayoutByRestaurant([FromQuery] long restaurantDTO)
        {
            var response = await _mediator.Send(new FindLayoutByRestaurantQuery(restaurantDTO));

            return ReturnResultHelper.ReturnQueryResult(response);
        }
    }
}
