using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TableTracker.Application.CQRS.Users.Commands.AddUser;
using TableTracker.Application.CQRS.Users.Commands.DeleteUser;
using TableTracker.Application.CQRS.Users.Commands.UpdateUser;
using TableTracker.Application.CQRS.Users.Queries.FilterUsers;
using TableTracker.Application.CQRS.Users.Queries.FindUserById;
using TableTracker.Application.CQRS.Users.Queries.GetAllUsers;
using TableTracker.Application.CQRS.Users.Queries.GetAllUsersByFullName;
using TableTracker.Application.CQRS.Users.Queries.GetUserByEmail;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Helpers;

namespace TableTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _mediator.Send(new GetAllUsersQuery());

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterUsers([FromQuery] string filter)
        {
            var response = await _mediator.Send(new FilterUsersQuery(filter));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] long id)
        {
            var response = await _mediator.Send(new FindUserByIdQuery(id));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetAllUsersByFullName([FromQuery] string name)
        {
            var response = await _mediator.Send(new GetAllUsersByFullNameQuery(name));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpGet("email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var response = await _mediator.Send(new GetUserByEmailQuery(email));

            return ReturnResultHelper.ReturnQueryResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDTO user)
        {
            var response = await _mediator.Send(new AddUserCommand(user));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
            var response = await _mediator.Send(new UpdateUserCommand(user));

            return ReturnResultHelper.ReturnCommandResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var response = await _mediator.Send(new DeleteUserCommand(id));

            return ReturnResultHelper.ReturnCommandResult(response);
        }
    }
}
