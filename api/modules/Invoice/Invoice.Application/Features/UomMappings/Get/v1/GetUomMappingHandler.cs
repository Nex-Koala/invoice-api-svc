using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Get.v1;

public sealed class GetUomMappingHandler(
    [FromKeyedServices("invoice:uomMappings")] IReadRepository<UomMapping> repository
) : IRequestHandler<GetUomMappingRequest, Response<UomMappingResponse>>
{
    public async Task<Response<UomMappingResponse>> Handle(
        GetUomMappingRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var uomMappingItem = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (uomMappingItem == null)
            throw new UomMappingNotFoundException(request.Id);

        var response = new UomMappingResponse(
            uomMappingItem.Id,
            uomMappingItem.UomId,
            uomMappingItem.LhdnUomCode
        );

        return new Response<UomMappingResponse>(response);
    }
}
