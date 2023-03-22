using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using TableTracker.Application.CQRS.Visitors.Commands.AddAvatar;
using TableTracker.Application.CQRS.Visitors.Commands.AddVisitor;
using TableTracker.Application.CQRS.Visitors.Commands.AddVisitorFavourite;
using TableTracker.Application.CQRS.Visitors.Commands.DeleteAvatar;
using TableTracker.Application.CQRS.Visitors.Commands.DeleteVisitorFavourite;
using TableTracker.Application.CQRS.Visitors.Commands.UpdateVisitor;
using TableTracker.Application.CQRS.Visitors.Queries.FindVisitorById;
using TableTracker.Application.CQRS.Visitors.Queries.FindVisitorFavouritesByVisitorId;
using TableTracker.Application.CQRS.Visitors.Queries.GetAllVisitors;
using TableTracker.Application.CQRS.Visitors.Queries.GetAllVisitorsByTrustFactor;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Helpers;

namespace TableTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/visitors")]
    public class VisitorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _host;

        public VisitorController(
            IMediator mediator,
            IWebHostEnvironment host)
        {
            _mediator = mediator;
            _host = host;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVisitors()
        {
            var response = await _mediator.Send(new GetAllVisitorsQuery());

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindVisitorById([FromRoute] long id)
        {
            var response = await _mediator.Send(new FindVisitorByIdQuery(id));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpGet("trust")]
        public async Task<IActionResult> GetAllVisitorsByTrustFactor(float trust)
        {
            var response = await _mediator.Send(new GetAllVisitorsByTrustFactorQuery(trust));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddVisitor([FromBody] VisitorDTO visitor)
        {
            var response = await _mediator.Send(new AddVisitorCommand(visitor));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVisitor([FromBody] VisitorDTO visitor)
        {
            visitor.DateOfBirth = visitor.DateOfBirth.AddHours(3);
            visitor.Reservations = null;
            visitor.Favourites = null;
            var response = await _mediator.Send(new UpdateVisitorCommand(visitor));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVisitor([FromBody] VisitorDTO visitor)
        {
            var response = await _mediator.Send(new AddVisitorCommand(visitor));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpGet("{id}/favourites")]
        public async Task<IActionResult> FindVisitorFavouriteByVisitorId([FromRoute] long id)
        {
            var response = await _mediator.Send(new FindVisitorFavouritesByVisitorIdQuery(id));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpPost("{id}/favourites")]
        public async Task<IActionResult> AddVisitorFavourite([FromRoute] long id, long restaurantId)
        {
            var response = await _mediator.Send(new AddVisitorFavouriteCommand(id, restaurantId));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpDelete("{id}/favourites")]
        public async Task<IActionResult> DeleteVisitorFavourite([FromRoute] long id, long restaurantId)
        {
            var response = await _mediator.Send(new DeleteVisitorFavouriteCommand(id, restaurantId));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpPost("{visitorId}/avatar")]
        public async Task<IActionResult> UploadAvatar(long visitorId)
        {
            var file = Request.Form.Files[0];

            if (file.Length > 0)
            {
                string extention = Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
                string fileName = Guid.NewGuid().ToString() + extention;
                string fullPath = Path.Combine(_host.WebRootPath, "images", fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var avatar = await _mediator.Send(new AddAvatarCommand(visitorId, fileName));

                return Ok(avatar.Object);
            }

            return BadRequest();
        }

        [HttpDelete("{visitorId}/avatar")]
        public async Task<IActionResult> DeleteAvatar(long visitorId)
        {
            var response = await _mediator.Send(new DeleteAvatarCommand(visitorId));

            if (response.Result == Domain.Enums.CommandResult.Success)
            {
                string fullPath = Path.Combine(_host.WebRootPath, "images", response.Object);

                System.IO.File.Delete(fullPath);

                return NoContent();
            }

            return BadRequest();
        }
    }
}
