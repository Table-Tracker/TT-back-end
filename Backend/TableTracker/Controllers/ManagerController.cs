using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TableTracker.Application.CQRS.Managers.Commands.AddManager;
using TableTracker.Application.CQRS.Managers.Commands.DeleteManager;
using TableTracker.Application.CQRS.Managers.Commands.UpdateManager;
using TableTracker.Application.CQRS.Managers.Queries.FindManagerById;
using TableTracker.Application.CQRS.Managers.Queries.FindManagerByRestaurant;
using TableTracker.Application.CQRS.Managers.Queries.GetAllManagers;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Helpers;

namespace TableTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/managers")]
    public class ManagerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManagers()
        {
            var response = await _mediator.Send(new GetAllManagersQuery());

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindManagerById([FromRoute] long id)
        {
            var response = await _mediator.Send(new FindManagerByIdQuery(id));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddManager([FromBody] ManagerDTO managerDTO)
        {
            var response = await _mediator.Send(new AddManagerCommand(managerDTO));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateManager([FromBody] ManagerDTO managerDTO)
        {
            var response = await _mediator.Send(new UpdateManagerCommand(managerDTO));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager([FromRoute] long id)
        {
            var response = await _mediator.Send(new DeleteManagerCommand(id));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpGet("restaurant")]
        public async Task<IActionResult> FindManagerByRestaurant([FromQuery] long restaurantDTO)
        {
            var response = await _mediator.Send(new FindManagerByRestaurantQuery(restaurantDTO));

            return ReturnResultHelper.ReturnQueryResult(response);
        }
    }
}
