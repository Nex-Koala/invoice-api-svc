using invoice_api_svc.Application.Features.ClassificationMapping.Commands.CreateClassificationMapping;
using invoice_api_svc.Application.Features.ClassificationMapping.Commands.DeleteClassificationMapping;
using invoice_api_svc.Application.Features.ClassificationMapping.Commands.UpdateClassificationMapping;
using invoice_api_svc.Application.Features.ClassificationMapping.Queries.GetAllClassificationMappings;
using invoice_api_svc.Application.Features.ClassificationMapping.Queries.GetClassificationMappingById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace invoice_api_svc.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/classification-mappings")]
    [ApiController]
    public class ClassificationMappingController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ClassificationMappingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all classification mappings by user ID
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllClassificationMappingsQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Get a classification mapping by ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetClassificationMappingByIdQuery { Id = id };
            var response = await _mediator.Send(query);

            if (response == null)
                return NotFound($"Classification mapping with ID {id} not found.");

            return Ok(response);
        }

        /// <summary>
        /// Create a new classification mapping
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassificationMappingCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = response.Data }, response);
        }

        /// <summary>
        /// Update an existing classification mapping
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClassificationMappingCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Delete a classification mapping by ID
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteClassificationMappingCommand { Id = id };
            var response = await _mediator.Send(command);

            if (!response.Succeeded)
                return NotFound($"Classification mapping with ID {id} not found.");

            return Ok(response);
        }
    }
}
