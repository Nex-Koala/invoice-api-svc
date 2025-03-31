using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.WebApi.Invoice.Application.Features.Classifications.Specifications;
using NexKoala.WebApi.Invoice.Application.Features.Classifications.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.GetList.v1;

public sealed class GetClassificationListHandler(
    [FromKeyedServices("invoice:classifications")] IReadRepository<Classification> repository
) : IRequestHandler<GetClassificationList, PagedResponse<ClassificationResponse>>
{
    public async Task<PagedResponse<ClassificationResponse>> Handle(
        GetClassificationList request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new ClassificationListFilterSpec(request.PageNumber, request.PageSize, request.UserId);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedResponse<ClassificationResponse>(
            items,
            request.PageNumber,
            request.PageSize,
            totalCount
        );
    }
}
