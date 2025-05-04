using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.GetByEmail.v1;

public sealed class GetPartnerByEmailHandler(
    [FromKeyedServices("invoice:partners")] IReadRepository<Partner> repository
) : IRequestHandler<GetPartnerByEmailRequest, Response<PartnerResponse>>
{
    public async Task<Response<PartnerResponse>> Handle(
        GetPartnerByEmailRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new PartnerByEmailSpec(request.Email);
        var partner = await repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (partner == null)
            throw new PartnerNotFoundException(request.Email);

        var response = partner.Adapt<PartnerResponse>();
        return new Response<PartnerResponse>(response);
    }
}
