using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
public class InvoiceDocumentResponse
{
    public Guid Id { get; set; }
    public string? Uuid { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public string DocumentCurrencyCode { get; set; }
    public string? TaxCurrencyCode { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalExcludingTax { get; set; }
    public decimal TotalIncludingTax { get; set; }
    public Guid SupplierId { get; set; }
    public SupplierDto Supplier { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerDto Customer { get; set; }
    public ICollection<InvoiceLineDto> InvoiceLines { get; set; }
    public bool SubmissionStatus { get; set; } = false;
    public string? DocumentStatus { get; set; }
}

public class InvoiceLineDto
{
    public Guid Id { get; set; }
    public string? LineNumber { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public string? Description { get; set; }
    public string? UnitCode { get; set; }
    public string? CurrencyCode { get; set; }
    public Guid InvoiceDocumentId { get; set; }
}

public class SupplierDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Tin { get; set; }
    public string? IdType { get; set; }
    public string? Brn { get; set; }
    public string? SstRegistrationNumber { get; set; }
    public string? TaxTourismRegistrationNumber { get; set; }
    public string? MsicCode { get; set; }
    public string? BusinessActivityDescription { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? ContactNumber { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? CountryCode { get; set; }
}

public class CustomerDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Tin { get; set; }
    public string? Brn { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? CountryCode { get; set; }
    public string? Email { get; set; }
    public string? ContactNumber { get; set; }
}
