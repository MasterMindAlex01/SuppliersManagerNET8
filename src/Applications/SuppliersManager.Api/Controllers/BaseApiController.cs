using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SuppliersManager.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public abstract class BaseApiController<T> : ControllerBase
    {
        private IMediator _mediatorInstance = null!;
        private ILogger<T> _loggerInstance = null!;
        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>()!;
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>()!;
    }
}
