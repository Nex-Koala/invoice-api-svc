using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Specifications;
using NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.GetList.v1;

public sealed class GetClassificationMappingListHandler(
    [FromKeyedServices("invoice:classificationMappings")] IReadRepository<ClassificationMapping> repository
) : IRequestHandler<GetClassificationMappingList, PagedResponse<ClassificationMappingResponse>>
{
    public async Task<PagedResponse<ClassificationMappingResponse>> Handle(
        GetClassificationMappingList request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new ClassificationMappingListFilterSpec(
            request.PageNumber,
            request.PageSize,
            request.UserId
        );

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedResponse<ClassificationMappingResponse>(
            items,
            request.PageNumber,
            request.PageSize,
            totalCount
        );
    }
}
