using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Caching;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.Get.v1;

public sealed class GetUomHandler(
    [FromKeyedServices("invoice:uoms")] IReadRepository<Uom> repository,
    ICacheService cache
) : IRequestHandler<GetUomRequest, Response<UomResponse>>
{
    public async Task<Response<UomResponse>> Handle(
        GetUomRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"uom:{request.Id}",
            async () =>
            {
                var uomItem = await repository.GetByIdAsync(request.Id, cancellationToken);
                if (uomItem == null)
                    throw new UomNotFoundException(request.Id);
                return new UomResponse(
                    uomItem.Id,
                    uomItem.UserId,
                    uomItem.Code,
                    uomItem.Description
                );
            },
            cancellationToken: cancellationToken
        );

        if (item == null)
            throw new UomNotFoundException(request.Id);

        return new Response<UomResponse>(item);
    }
}
