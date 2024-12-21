using invoice_api_svc.Application.Features.UomMappings.Commands.CreateUomMapping;
using invoice_api_svc.Application.Features.UomMappings.Commands.DeleteUomMapping;
using invoice_api_svc.Application.Features.UomMappings.Commands.UpdateUomMapping;
using invoice_api_svc.Application.Features.UomMappings.Queries.GetAllUomMappings;
using invoice_api_svc.Application.Features.UomMappings.Queries.GetUomMappingById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace invoice_api_svc.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/uom-mappings")]
    [ApiController]
    public class UOMMappingController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UOMMappingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all UOM mappings with optional pagination
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUomMappingsQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Get a UOM mapping by ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUomMappingByIdQuery { Id = id };
            var response = await _mediator.Send(query);

            if (response == null)
                return NotFound($"UOM mapping with ID {id} not found.");

            return Ok(response);
        }

        /// <summary>
        /// Create a new UOM mapping
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUomMappingCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = response.Data }, response);
        }

        /// <summary>
        /// Update an existing UOM mapping
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUomMappingCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Delete a UOM mapping by ID
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUomMappingCommand { Id = id };
            var response = await _mediator.Send(command);

            if (!response.Succeeded)
                return NotFound($"UOM mapping with ID {id} not found.");

            return Ok(response);
        }
    }
}
