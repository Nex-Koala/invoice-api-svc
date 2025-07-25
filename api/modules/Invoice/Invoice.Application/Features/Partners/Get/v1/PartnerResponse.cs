using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;

public sealed record PartnerResponse(
    Guid Id,
    string UserId,
    string Name,
    string CompanyName,
    string Tin,
    string SchemeId,
    string RegistrationNumber,
    string SstRegistrationNumber,
    string TourismTaxRegistrationNumber,
    string Email,
    string Phone,
    string MsicCode,
    string BusinessActivityDescription,
    string Address1,
    string Address2,
    string Address3,
    string PostalCode,
    string City,
    string State,
    string CountryCode,
    bool Status,
    LicenseKeyResponseDto? LicenseKey
);
