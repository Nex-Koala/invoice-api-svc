using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Partners.Update.v1;

public sealed record UpdateProfileCommand(
    Guid Id,
    string? Name,
    string? Tin,
    string? SchemeID,
    string? RegistrationNumber,
    string? SSTRegistrationNumber,
    string? TourismTaxRegistrationNumber,
    string? Email,
    string? Phone,
    string? MSICCode,
    string? BusinessActivityDescription,
    string? Address1,
    string? Address2,
    string? Address3,
    string? PostalCode,
    string? City,
    string? State,
    string? CountryCode
) : IRequest<Response<Guid>>;
