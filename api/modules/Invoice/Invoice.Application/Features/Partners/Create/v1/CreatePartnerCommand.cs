using System.ComponentModel;
using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Create.v1;

public sealed record CreatePartnerCommand(
    [property: DefaultValue("Partner Name")] string Name,
    [property: DefaultValue("Company Name")] string CompanyName,
    [property: DefaultValue("Address Line 1")] string Address1,
    [property: DefaultValue("Address Line 2")] string? Address2,
    [property: DefaultValue("Address Line 3")] string? Address3,
    [property: DefaultValue("Email Address")] string Email,
    [property: DefaultValue("Phone Number")] string Phone,
    [property: DefaultValue(true)] bool Status,
    LicenseRequestDto LicenseKey
) : IRequest<Response<Guid>>;
