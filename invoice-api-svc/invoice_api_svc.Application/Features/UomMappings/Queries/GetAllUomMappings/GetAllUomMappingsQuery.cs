using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace invoice_api_svc.Application.Features.UomMappings.Queries.GetAllUomMappings
{
    public class GetAllUomMappingsQuery : IRequest<Response<IEnumerable<UomMappingViewModel>>>
    {
    }
}
