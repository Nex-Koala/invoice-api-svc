using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Caching;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.GetByEmail.v1;

public sealed class GetPartnerByEmailHandler(
    [FromKeyedServices("invoice:partners")] IReadRepository<Partner> repository,
    ICacheService cache
) : IRequestHandler<GetPartnerByEmailRequest, Response<PartnerResponse>>
{
    public async Task<Response<PartnerResponse>> Handle(
        GetPartnerByEmailRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"partner:{request.Email}",
            async () =>
            {
                var spec = new PartnerByEmailSpec(request.Email);
                var partner = await repository.FirstOrDefaultAsync(spec, cancellationToken);
                if (partner == null)
                    throw new PartnerNotFoundException(request.Email);
                return partner.Adapt<PartnerResponse>();
            },
            cancellationToken: cancellationToken
        );

        if (item == null)
            throw new PartnerNotFoundException(request.Email);

        return new Response<PartnerResponse>(item);
    }
}
