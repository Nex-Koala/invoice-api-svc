using invoice_api_svc.Application.Features.Uoms.Commands.CreateUom;
using invoice_api_svc.Application.Features.Uoms.Commands.DeleteUomById;
using invoice_api_svc.Application.Features.Uoms.Commands.UpdateUom;
using invoice_api_svc.Application.Features.Uoms.Queries.GetAllUoms;
using invoice_api_svc.Application.Features.Uoms.Queries.GetUomById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace invoice_api_svc.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/uoms")]
    [ApiController]
    public class UOMController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UOMController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all UOMs (paginated)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUoms([FromQuery] GetAllUomsQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Get a UOM by ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUomById(int id)
        {
            var query = new GetUomByIdQuery { Id = id };
            var response = await _mediator.Send(query);

            if (response == null)
                return NotFound($"UOM with ID {id} not found.");

            return Ok(response);
        }

        /// <summary>
        /// Create a new UOM
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUom([FromBody] CreateUomCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUomById), new { id = response.Data }, response);
        }

        /// <summary>
        /// Update an existing UOM
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUom(int id, [FromBody] UpdateUomCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Delete a UOM by ID (soft delete)
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUom(int id)
        {
            var command = new DeleteUomByIdCommand { Id = id };
            var response = await _mediator.Send(command);

            if (!response.Succeeded)
                return NotFound($"UOM with ID {id} not found.");

            return Ok(response);
        }
    }
}
