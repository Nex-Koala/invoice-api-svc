using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class Supplier : BaseEntity, IAggregateRoot
{
    public string? Name { get; set; } // "Supplier's Name"

    public string? Tin { get; set; } // Supplier's TIN

    public string? Brn { get; set; } // Supplier's BRN

    public string? Address { get; set; } // Supplier's Address

    public string? City { get; set; } // "Kuala Lumpur"

    public string? PostalCode { get; set; } // "50480"

    public string? CountryCode { get; set; } // "MYS"
}
