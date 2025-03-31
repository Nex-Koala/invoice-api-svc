using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.GetList.v1;

public sealed record GetPartnerList(
    int PageNumber = 1,
    int PageSize = 20,
    string? Name = null,
    string? CompanyName = null,
    string? Email = null,
    string? Phone = null,
    string? LicenseKey = null,
    bool? Status = null
) : IRequest<PagedResponse<PartnerResponse>>;
