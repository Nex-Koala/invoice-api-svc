using System.Numerics;
using System.Xml.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.GetList.v1;

public sealed class GetPartnerListHandler(
    [FromKeyedServices("invoice:partners")] IReadRepository<Partner> repository
) : IRequestHandler<GetPartnerList, PagedResponse<PartnerResponse>>
{
    public async Task<PagedResponse<PartnerResponse>> Handle(
        GetPartnerList request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new PartnerListFilterSpec(
            request.PageNumber,
            request.PageSize,
            request.Name,
            request.CompanyName,
            request.Email,
            request.Phone,
            request.LicenseKey,
            request.Status
        );

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedResponse<PartnerResponse>(
            items,
            request.PageNumber,
            request.PageSize,
            totalCount
        );
    }
}
