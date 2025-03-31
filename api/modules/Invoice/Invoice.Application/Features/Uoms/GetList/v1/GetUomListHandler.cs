using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Uoms.Get.v1;
using NexKoala.WebApi.Invoice.Application.Features.Uoms.Specifications;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.GetList.v1;

public sealed class GetUomListHandler(
    [FromKeyedServices("invoice:uoms")] IReadRepository<Uom> repository
) : IRequestHandler<GetUomList, PagedResponse<UomResponse>>
{
    public async Task<PagedResponse<UomResponse>> Handle(
        GetUomList request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new UomListFilterSpec(request.PageNumber, request.PageSize, request.UserId);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedResponse<UomResponse>(
            items,
            request.PageNumber,
            request.PageSize,
            totalCount
        );
    }
}
