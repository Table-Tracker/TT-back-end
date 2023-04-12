using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using TableTracker.Application.CQRS.Notifications.Commands.SendFAQEmail;
using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("faq-email")]
        public async Task<IActionResult> SendFAQEmail([FromBody] EmailDTO email)
        {
            var response = await _mediator.Send(new SendFAQEmailCommand(email));
            return Ok(response);
        }
    }
}
