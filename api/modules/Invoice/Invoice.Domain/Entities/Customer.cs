using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class Customer : BaseEntity, IAggregateRoot
{
    public string? Name { get; set; } // "Buyer's Name"

    public string? Tin { get; set; } // Buyer's TIN

    public string? Brn { get; set; } // Buyer's BRN

    public string? Address { get; set; } // Buyer's Address

    public string? City { get; set; } // "Kuala Lumpur"

    public string? PostalCode { get; set; } // "50480"

    public string? CountryCode { get; set; } // "MYS"
}
