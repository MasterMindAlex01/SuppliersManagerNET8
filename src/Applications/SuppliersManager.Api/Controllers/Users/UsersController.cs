using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuppliersManager.Application.Features.Users.Commands;
using SuppliersManager.Application.Features.Users.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuppliersManager.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController<UsersController>
    {
        // GET: api/<UsersController>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll([FromQuery] GetAllUsersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/<UsersController>/5
        [HttpGet("GetUserById")]
        public async Task<ActionResult> GetUserById([FromQuery] string id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // POST api/<UsersController>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // PUT api/<UsersController>/5
        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromQuery] string id)
        {
            var command = new DeleteUserCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
