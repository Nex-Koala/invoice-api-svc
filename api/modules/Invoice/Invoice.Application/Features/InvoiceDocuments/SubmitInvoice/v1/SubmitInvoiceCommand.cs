using MediatR;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Invoice;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.SubmitInvoice.v1;

public class SubmitInvoiceCommand: IRequest<object> { 
    public string? UserId { get; set; }
    public List<InvoiceRequest> Invoices { get; set; } = new();
}
