using invoice_api_svc.Application.Features.UomMappings.Commands.CreateUomMapping;
using invoice_api_svc.Application.Features.UomMappings.Commands.DeleteUomMapping;
using invoice_api_svc.Application.Features.UomMappings.Queries.GetAllUomMappings;
using invoice_api_svc.Application.Features.UomMappings.Queries.GetUomMappingById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace invoice_api_svc.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UOMMappingController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UOMMappingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUomMappingsQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUomMappingByIdQuery { Id = id };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromBody] CreateUomMappingCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUomMappingCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
