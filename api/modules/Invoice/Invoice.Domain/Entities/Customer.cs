using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class Customer : BaseEntity, IAggregateRoot
{
    public string? Name { get; set; } // "Buyer's Name"

    public string? Tin { get; set; } // Buyer's TIN
    public string? IdType { get; set; }
    public string? Brn { get; set; } // Buyer's BRN

    public string? Address1 { get; set; } // Buyer's Address line 1
    public string? Address2 { get; set; } // Buyer's Address line 2
    public string? Address3 { get; set; } // Buyer's Address line 3

    public string? City { get; set; } // "Kuala Lumpur"

    public string? PostalCode { get; set; } // "50480"

    public string? CountryCode { get; set; } // "MYS"
    public string? Email { get; set; }
    public string? ContactNumber { get; set; }
    public string? SstRegistrationNumber { get; set; }
}
