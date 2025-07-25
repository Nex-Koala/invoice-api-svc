using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Create.v1;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Update.v1;

public sealed record UpdatePartnerCommand(
    Guid Id,
    string? Name,
    string? CompanyName,
    string? Address1,
    string? Address2,
    string? Address3,
    string? Email,
    string? Phone,
    bool? Status,
    LicenseRequestDto? LicenseKey
) : IRequest<Response<Guid>>;
