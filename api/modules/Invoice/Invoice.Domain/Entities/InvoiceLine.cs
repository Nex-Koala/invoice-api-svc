using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class InvoiceLine : BaseEntity, IAggregateRoot
{
    public string? LineNumber { get; set; } // "1234"
    public decimal Quantity { get; set; } // Invoiced Quantity
    public decimal UnitPrice { get; set; } // Price per unit
    public decimal LineAmount { get; set; } // LineExtensionAmount
    public decimal TaxAmount { get; set; }
    public string? Description { get; set; } // Item description
    public string? UnitCode { get; set; } // "C62" - Unit code for invoiced quantity
    public string? ClassificationCode { get; set; } // Classification code for the item
    public string? CurrencyCode { get; set; } // "MYR"
    public Guid InvoiceDocumentId { get; set; }
    public InvoiceDocument InvoiceDocument { get; set; }
}
