using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;

public sealed class GetPartnerHandler(
    [FromKeyedServices("invoice:partners")] IReadRepository<Partner> repository
) : IRequestHandler<GetPartnerRequest, Response<PartnerResponse>>
{
    public async Task<Response<PartnerResponse>> Handle(
        GetPartnerRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var partner = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (partner == null)
            throw new PartnerNotFoundException(request.Id);

        var response = partner.Adapt<PartnerResponse>();
        return new Response<PartnerResponse>(response);
    }
}
