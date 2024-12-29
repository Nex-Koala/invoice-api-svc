using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using invoice_api_svc.Application.Features.Partners.Commands.UpdatePartner;
using invoice_api_svc.Application.DTOs.User;
using invoice_api_svc.Application.Features.Partners.Queries.GetPartnerByEmail;

namespace invoice_api_svc.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v1/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Get profile details
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetProfile([FromQuery] string email)
        {
            var query = new GetPartnerByEmailQuery { Email = email };
            var response = await _mediator.Send(query);

            if (response == null)
            {
                return NotFound($"Partner with Email {email} not found");
            }

            return Ok(response);
        }

        /// <summary>
        /// Update an user profile
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UserUpdateProfileDto request)
        {
            if (id != request.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            var command = new UpdatePartnerCommand() { IsAdmin = false, UpdateDto = request };

            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
