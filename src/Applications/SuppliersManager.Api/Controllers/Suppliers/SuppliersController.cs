using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuppliersManager.Application.Features.Suppliers.Commands;
using SuppliersManager.Application.Features.Suppliers.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuppliersManager.Api.Controllers.Suppliers
{
    [Authorize]
    public class SuppliersController : BaseApiController<SuppliersController>
    {
        // GET: api/<SuppliersController>
        [HttpGet("GetAll")]
        public async Task<ActionResult> Get([FromQuery] GetAllSuppliersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/<SuppliersController>/5
        [HttpGet("GetSupplierById")]
        public async Task<ActionResult> Get([FromQuery] string id)
        {
            var query = new GetSupplierByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // POST api/<SuppliersController>
        [HttpPost("create")]
        public async Task<ActionResult> Post([FromBody] CreateSupplierCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // PUT api/<SuppliersController>/5
        [HttpPut("update")]
        public async Task<ActionResult> Put([FromBody] UpdateSupplierCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromQuery] string id)
        {
            var command = new DeleteSupplierCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
