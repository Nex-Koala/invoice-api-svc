using invoice_api_svc.Application.Features.Products.Commands.DeleteUomById;
using invoice_api_svc.Application.Features.Products.Commands.UpdateUom;
using invoice_api_svc.Application.Features.Products.Queries.GetAllUoms;
using invoice_api_svc.Application.Features.Products.Queries.GetUomById;
using invoice_api_svc.Application.Features.Uoms.Commands.CreateUom;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace invoice_api_svc.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class UOMController : BaseApiController
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUomById(int id)
        {
            var query = new GetUomByIdQuery { Id = id };
            var response = await _mediator.Send(query);
            return response != null ? Ok(response) : NotFound($"UOM with ID {id} not found.");
        }

        /// <summary>
        /// Create a new UOM
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUom([FromBody] CreateUomCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Update an existing UOM
        /// </summary>
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUom([FromBody] UpdateUomCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Delete a UOM by ID (soft delete)
        /// </summary>
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUom([FromBody] DeleteUomByIdCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
