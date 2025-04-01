using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Get.v1;

public class GetUomMappingRequest : IRequest<Response<UomMappingResponse>>
{
    public Guid Id { get; set; }

    public GetUomMappingRequest(Guid id) => Id = id;
}
