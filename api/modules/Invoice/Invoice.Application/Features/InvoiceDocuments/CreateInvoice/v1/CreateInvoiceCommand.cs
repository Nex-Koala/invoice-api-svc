using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.CreateInvoice.v1;

public partial record CreateInvoiceCommand(
    string InvoiceNumber,
    DateTime IssueDate,
    decimal TotalAmount,
    decimal TaxAmount,
    string SupplierName,
    List<InvoiceLineDto> InvoiceLines
) : IRequest<Response<Guid>>;
