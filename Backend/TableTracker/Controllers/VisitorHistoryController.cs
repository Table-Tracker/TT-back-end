using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TableTracker.Application.CQRS.VisitorHistories.Commands.AddVisit;
using TableTracker.Application.CQRS.VisitorHistories.Commands.DeleteVisit;
using TableTracker.Application.CQRS.VisitorHistories.Commands.UpdateVisit;
using TableTracker.Application.CQRS.VisitorHistories.Queries.FindVisitById;
using TableTracker.Application.CQRS.VisitorHistories.Queries.GetAllVisits;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Helpers;

namespace TableTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/visits")]
    public class VisitorHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VisitorHistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVisits()
        {
            var response = await _mediator.Send(new GetAllVisitsQuery());

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindVisitById([FromRoute] long id)
        {
            var response = await _mediator.Send(new FindVisitByIdQuery(id));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddVisit([FromQuery] VisitorHistoryDTO visit)
        {
            var response = await _mediator.Send(new AddVisitCommand(visit));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVisit([FromQuery] VisitorHistoryDTO visit)
        {
            var response = await _mediator.Send(new UpdateVisitCommand(visit));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisit([FromRoute] long id)
        {
            var response = await _mediator.Send(new DeleteVisitCommand(id));

            return ReturnResultHelper.ReturnCommandResult(response);
        }
    }
}
