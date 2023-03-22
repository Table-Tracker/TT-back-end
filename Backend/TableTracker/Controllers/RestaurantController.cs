using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TableTracker.Application.CQRS.Restaurants.Commands.AddRestaurant;
using TableTracker.Application.CQRS.Restaurants.Commands.DeleteRestaurant;
using TableTracker.Application.CQRS.Restaurants.Commands.UpdateRestaurant;
using TableTracker.Application.CQRS.Restaurants.Queries.FindRestaurantById;
using TableTracker.Application.CQRS.Restaurants.Queries.GetAllRestaurants;
using TableTracker.Application.CQRS.Restaurants.Queries.GetAllRestaurantsWithFiltering;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Helpers;

namespace TableTracker.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindRestaurantById([FromRoute] long id)
        {
            var response = await _mediator.Send(new FindRestaurantByIdQuery(id));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRestaurant([FromBody] RestaurantDTO restaurant)
        {
            var response = await _mediator.Send(new AddRestaurantCommand(restaurant));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantDTO restaurant)
        {
            var response = await _mediator.Send(new UpdateRestaurantCommand(restaurant));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] long id)
        {
            var response = await _mediator.Send(new DeleteRestaurantCommand(id));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var response = await _mediator.Send(new GetAllRestaurantsQuery());

            return ReturnResultHelper.ReturnQueryResult(response);
        }
    }
}
