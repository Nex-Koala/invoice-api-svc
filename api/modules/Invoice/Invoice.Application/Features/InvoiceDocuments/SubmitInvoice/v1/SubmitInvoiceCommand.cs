using MediatR;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Invoice;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.SubmitInvoice.v1;

public partial class SubmitInvoiceCommand : InvoiceRequest, IRequest<object> { 
    public string? UserId { get; set; }
}
