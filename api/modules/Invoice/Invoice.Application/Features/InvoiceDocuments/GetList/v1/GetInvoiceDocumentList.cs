using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetList.v1;

public record GetInvoiceDocumentList(
    int PageNumber = 1,
    int PageSize = 20,
    string? Uuid = null,
    bool? Status = null,
    DateTime? IssueDateFrom = null,
    DateTime? IssueDateTo = null
) : IRequest<PagedResponse<InvoiceDocumentResponse>>
{
    public string? UserId { get; init; }
};
