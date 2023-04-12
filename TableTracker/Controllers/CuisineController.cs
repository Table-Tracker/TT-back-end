using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TableTracker.Application.CQRS.Cuisines.Commands.AddCuisine;
using TableTracker.Application.CQRS.Cuisines.Commands.DeleteCuisine;
using TableTracker.Application.CQRS.Cuisines.Commands.UpdateCuisine;
using TableTracker.Application.CQRS.Cuisines.Queries.FindCuisineById;
using TableTracker.Application.CQRS.Cuisines.Queries.GetAllCuisines;
using TableTracker.Application.CQRS.Cuisines.Queries.GetCuisineByName;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Helpers;

namespace TableTracker.Controllers
{
    [ApiController]
    [Route("api/cuisines")]
    public class CuisineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CuisineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCuisines()
        {
            var response = await _mediator.Send(new GetAllCuisinesQuery());

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindCuisineById([FromRoute] long id)
        {
            var response = await _mediator.Send(new FindCuisineByIdQuery(id));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpGet("name")]
        public async Task<IActionResult> FindCuisineByName([FromQuery] string name)
        {
            var response = await _mediator.Send(new GetCuisineByNameQuery(name));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCuisine([FromBody] CuisineDTO cuisine)
        {
            var response = await _mediator.Send(new AddCuisineCommand(cuisine));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateCuisine([FromBody] CuisineDTO cuisine)
        {
            var response = await _mediator.Send(new UpdateCuisineCommand(cuisine));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuisine([FromRoute] long id)
        {
            var response = await _mediator.Send(new DeleteCuisineCommand(id));

            return ReturnResultHelper.ReturnCommandResult(response);
        }
    }
}
