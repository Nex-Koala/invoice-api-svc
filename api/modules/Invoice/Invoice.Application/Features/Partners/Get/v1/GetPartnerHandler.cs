using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Caching;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;

public sealed class GetPartnerHandler(
    [FromKeyedServices("invoice:partners")] IReadRepository<Partner> repository,
    ICacheService cache
) : IRequestHandler<GetPartnerRequest, Response<PartnerResponse>>
{
    public async Task<Response<PartnerResponse>> Handle(
        GetPartnerRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"partner:{request.Id}",
            async () =>
            {
                var partner = await repository.GetByIdAsync(request.Id, cancellationToken);
                if (partner == null)
                    throw new PartnerNotFoundException(request.Id);
                return partner.Adapt<PartnerResponse>();
            },
            cancellationToken: cancellationToken
        );

        if (item == null)
            throw new PartnerNotFoundException(request.Id);

        return new Response<PartnerResponse>(item);
    }
}
