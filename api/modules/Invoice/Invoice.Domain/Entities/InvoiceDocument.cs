using System;
using System.Collections.Generic;
using NexKoala.Framework.Core.Domain;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.WebApi.Invoice.Domain.Entities;

public class InvoiceDocument : BaseEntity, IAggregateRoot
{
    public string? Uuid { get; set; }
    public string InvoiceNumber { get; set; } // "JSON-INV12345"
    public DateTime IssueDate { get; set; } // "2024-07-23"
    public string DocumentCurrencyCode { get; set; } // "MYR"
    public string? TaxCurrencyCode { get; set; } // "MYR"
    public decimal TotalAmount { get; set; } // Total Payable Amount
    public decimal TaxAmount { get; set; } // Tax Total
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } // Supplier Info
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } // Customer Info
    public ICollection<InvoiceLine> InvoiceLines { get; set; } // Invoice Line Items
    public bool SubmissionStatus { get; set; } = false; // Submission status
}
