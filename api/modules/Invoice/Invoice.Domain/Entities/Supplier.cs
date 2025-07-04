﻿using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class Supplier : BaseEntity, IAggregateRoot
{
    public string? Name { get; set; } // "Supplier's Name"
    public string? Tin { get; set; } // Supplier's TIN
    public string? IdType { get; set; }
    public string? Brn { get; set; } // Supplier's BRN
    public string? SstRegistrationNumber { get; set; }
    public string? TaxTourismRegistrationNumber { get; set; }
    public string? MsicCode { get; set; }
    public string? BusinessActivityDescription { get; set; }
    public string? Address { get; set; } // Supplier's Address
    public string? Email { get; set; }
    public string? ContactNumber { get; set; }
    public string? City { get; set; } // "Kuala Lumpur"
    public string? PostalCode { get; set; } // "50480"
    public string? CountryCode { get; set; } // "MYS"
}
