using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.RecentDocument;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRecentDocuments.v1;

public partial class GetRecentDocuments : IRequest<Response<RecentDocuments>>
{
    public int PageNo { get; set; } = 1;  // Default value set to 1
    public int PageSize { get; set; } = 10;  // Default value set to 10

    public string? SubmissionDateFrom { get; set; }
    public string? SubmissionDateTo { get; set; }
    public string? IssueDateFrom { get; set; }
    public string? IssueDateTo { get; set; }
    public string? Direction { get; set; }
    public string? Status { get; set; }
    public string? DocumentType { get; set; }
    public string? ReceiverIdType { get; set; }
    public string? ReceiverId { get; set; }
    public string? ReceiverTin { get; set; }
    public string? IssuerTin { get; set; }
    public string? IssuerIdType { get; set; }
    public string? IssuerId { get; set; }
    public string? UserId { get; set; } = default!;
}
