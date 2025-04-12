using System.Numerics;
using System.Xml.Linq;
using NexKoala.Framework.Core;
using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class Partner : AuditableEntity, IAggregateRoot
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string? Tin { get; set; }
    public string? SchemeId { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? SstRegistrationNumber { get; set; }
    public string? TourismTaxRegistrationNumber { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string? MsicCode { get; set; }
    public string? BusinessActivityDescription { get; set; }
    public string Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? CountryCode { get; set; }
    public string LicenseKey { get; set; }
    public bool Status { get; set; }
    public int SubmissionCount { get; set; }
    public int MaxSubmissions { get; set; }

    public static Partner Create(
        string userId,
        string name,
        string companyName,
        string address1,
        string? address2,
        string? address3,
        string email,
        string phone,
        string licenseKey,
        bool status,
        int maxSubmissions
    )
    {
        return new Partner
        {
            UserId = userId,
            Name = name,
            CompanyName = companyName,
            Address1 = address1,
            Address2 = address2,
            Address3 = address3,
            Email = email,
            Phone = phone,
            LicenseKey = licenseKey,
            Status = status,
            MaxSubmissions = maxSubmissions,
        };
    }

    public Partner Update(
        string? name,
        string? companyName,
        string? address1,
        string? address2,
        string? address3,
        string? email,
        string? phone,
        string? licenseKey,
        bool? status,
        int? maxSubmissions
    )
    {
        if (name is not null && Name?.Equals(name, StringComparison.OrdinalIgnoreCase) is not true)
            Name = name;

        if (companyName is not null && CompanyName?.Equals(companyName, StringComparison.OrdinalIgnoreCase) is not true)
            CompanyName = companyName;

        if (address1 is not null && Address1?.Equals(address1, StringComparison.OrdinalIgnoreCase) is not true)
            Address1 = address1;

        if (address2 is not null && Address2?.Equals(address2, StringComparison.OrdinalIgnoreCase) is not true)
            Address2 = address2;

        if (address3 is not null && Address3?.Equals(address3, StringComparison.OrdinalIgnoreCase) is not true)
            Address3 = address3;

        if (email is not null && Email?.Equals(email, StringComparison.OrdinalIgnoreCase) is not true)
            Email = email;

        if (phone is not null && Phone?.Equals(phone, StringComparison.OrdinalIgnoreCase) is not true)
            Phone = phone;

        if (licenseKey is not null && LicenseKey?.Equals(licenseKey, StringComparison.OrdinalIgnoreCase) is not true)
            LicenseKey = licenseKey;

        if (status is not null && Status != status.Value)
            Status = status.Value;

        if (maxSubmissions is not null && MaxSubmissions != maxSubmissions.Value)
            MaxSubmissions = maxSubmissions.Value;

        return this;
    }

    public Partner ProfileUpdate(
        string? name,
        string? tin,
        string? schemeID,
        string? registrationNumber,
        string? sstRegistrationNumber,
        string? tourismTaxRegistrationNumber,
        string? email,
        string? phone,
        string? msicCode,
        string? businessActivityDescription,
        string? address1,
        string? address2,
        string? address3,
        string? postalCode,
        string? city,
        string? state,
        string? countryCode
    )
    {
        if (name is not null && Name?.Equals(name, StringComparison.OrdinalIgnoreCase) is not true)
            Name = name;

        if (tin is not null && Tin?.Equals(tin, StringComparison.OrdinalIgnoreCase) is not true)
            Tin = tin;

        if (schemeID is not null && SchemeId?.Equals(schemeID, StringComparison.OrdinalIgnoreCase) is not true)
            SchemeId = schemeID;

        if (registrationNumber is not null && RegistrationNumber?.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase) is not true)
            RegistrationNumber = registrationNumber;

        if (sstRegistrationNumber is not null && SstRegistrationNumber?.Equals(sstRegistrationNumber, StringComparison.OrdinalIgnoreCase) is not true)
            SstRegistrationNumber = sstRegistrationNumber;

        if (tourismTaxRegistrationNumber is not null && TourismTaxRegistrationNumber?.Equals(tourismTaxRegistrationNumber, StringComparison.OrdinalIgnoreCase) is not true)
            TourismTaxRegistrationNumber = tourismTaxRegistrationNumber;

        if (email is not null && Email?.Equals(email, StringComparison.OrdinalIgnoreCase) is not true)
            Email = email;

        if (phone is not null && Phone?.Equals(phone, StringComparison.OrdinalIgnoreCase) is not true)
            Phone = phone;

        if (msicCode is not null && MsicCode?.Equals(msicCode, StringComparison.OrdinalIgnoreCase) is not true)
            MsicCode = msicCode;

        if (businessActivityDescription is not null && BusinessActivityDescription?.Equals(businessActivityDescription, StringComparison.OrdinalIgnoreCase) is not true)
            BusinessActivityDescription = businessActivityDescription;

        if (address1 is not null && Address1?.Equals(address1, StringComparison.OrdinalIgnoreCase) is not true)
            Address1 = address1;

        if (address2 is not null && Address2?.Equals(address2, StringComparison.OrdinalIgnoreCase) is not true)
            Address2 = address2;

        if (address3 is not null && Address3?.Equals(address3, StringComparison.OrdinalIgnoreCase) is not true)
            Address3 = address3;

        if (postalCode is not null && PostalCode?.Equals(postalCode, StringComparison.OrdinalIgnoreCase) is not true)
            PostalCode = postalCode;

        if (city is not null && City?.Equals(city, StringComparison.OrdinalIgnoreCase) is not true)
            City = city;

        if (state is not null && State?.Equals(state, StringComparison.OrdinalIgnoreCase) is not true)
            State = state;

        if (countryCode is not null && CountryCode?.Equals(countryCode, StringComparison.OrdinalIgnoreCase) is not true)
            CountryCode = countryCode;

        return this;
    }
}
