using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.UomMappings.Get.v1;
using NexKoala.WebApi.Invoice.Application.Features.UomMappings.Specifications;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.GetList.v1;

public sealed class GetUomMappingListHandler(
    [FromKeyedServices("invoice:uomMappings")] IReadRepository<UomMapping> repository
) : IRequestHandler<GetUomMappingList, PagedResponse<UomMappingResponse>>
{
    public async Task<PagedResponse<UomMappingResponse>> Handle(
        GetUomMappingList request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new UomMappingListFilterSpec(
            request.PageNumber,
            request.PageSize,
            request.UserId
        );

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedResponse<UomMappingResponse>(
            items,
            request.PageNumber,
            request.PageSize,
            totalCount
        );
    }
}
