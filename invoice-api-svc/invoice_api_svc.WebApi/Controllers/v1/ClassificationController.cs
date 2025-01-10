using invoice_api_svc.Application.Features.Classification.Commands.CreateClassification;
using invoice_api_svc.Application.Features.Classification.Commands.DeleteClassificationById;
using invoice_api_svc.Application.Features.Classification.Commands.UpdateClassification;
using invoice_api_svc.Application.Features.Classification.Queries.GetAllClassifications;
using invoice_api_svc.Application.Features.Classification.Queries.GetClassificationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace invoice_api_svc.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/classifications")]
    [ApiController]
    public class ClassificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClassificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all classification code (paginated)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllClassifications([FromQuery] GetAllClassificationQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Get a classification code by ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetClassificationById(int id)
        {
            var query = new GetClassificationByIdQuery { Id = id };
            var response = await _mediator.Send(query);

            if (response == null)
                return NotFound($"Classification with ID {id} not found.");

            return Ok(response);
        }

        /// <summary>
        /// Create a new classification code
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateClassification([FromBody] CreateClassificationCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetClassificationById), new { id = response.Data }, response);
        }

        /// <summary>
        /// Update an existing classification code
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateClassification(int id, [FromBody] UpdateClassificationCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Delete a clssification code by ID (soft delete)
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteClassification(int id)
        {
            var command = new DeleteClassificationByIdCommand { Id = id };
            var response = await _mediator.Send(command);

            if (!response.Succeeded)
                return NotFound($"Classification with ID {id} not found.");

            return Ok(response);
        }
    }
}
