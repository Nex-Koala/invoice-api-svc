using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using invoice_api_svc.Application.Features.Partners.Commands.CreatePartner;
using invoice_api_svc.Application.Features.Partners.Commands.DeletePartner;
using invoice_api_svc.Application.Features.Partners.Commands.UpdatePartner;
using invoice_api_svc.Application.Features.Partners.Queries.GetAllPartners;
using invoice_api_svc.Application.Features.Partners.Queries.GetPartnerById;
using invoice_api_svc.Application.DTOs.User;
using invoice_api_svc.Application.Features.Partners.Queries.GetPartnerByEmail;

namespace invoice_api_svc.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v1/partners")]
    public class PartnerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PartnerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all Partners (paginated)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllPartners([FromQuery] GetAllPartnersQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Get partner by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPartnerById(int id)
        {
            var query = new GetPartnerByIdQuery { Id = id };
            var response = await _mediator.Send(query);

            if (response == null) {
                return NotFound($"Partner with ID {id} not found");
            }

            return Ok(response);
        }

        /// <summary>
        /// Create a new partner
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePartner([FromBody] CreatePartnerCommand command)
        {
            var response = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetPartnerById), new { id = response.Data }, response);
        }

        /// <summary>
        /// Update an existing partner
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePartner(int id, [FromBody] AdminUpdatePartnerDto request)
        {
            if (id != request.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            var command = new UpdatePartnerCommand() { IsAdmin = true, UpdateDto = request };

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Delete a Partner by ID 
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartner(int id)
        {
            var command = new DeletePartnerByIdCommand { Id = id };
            var response = await _mediator.Send(command);
            if(!response.Succeeded)
                return NotFound($"Partner with ID {id} not found.");

            return Ok(response);
        }
    }
}
