using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuppliersManager.Application.Features.Auth.Commands;

namespace SuppliersManager.Api.Controllers
{
    public class AuthController : BaseApiController<AuthController>
    {        
        // POST v1/Auth
        [HttpPost("login")]
        public async Task<IActionResult> LoginToken(TokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("authenticated")]
        public IActionResult Authenticated()
        {

            var data = HttpContext.User.Claims.Select(x => new
            {
                x.Value,
                x.Type
            });

            return Ok(new
            {
                data
            });
        }
    }
}
