using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetList.v1;

public record GetInvoiceDocumentList : IRequest<PagedResponse<InvoiceDocumentResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public string? Uuid { get; init; }
    public bool? Status { get; init; }
    public DateTime? IssueDateFrom { get; init; }
    public DateTime? IssueDateTo { get; init; }
    public string? UserId { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DocumentStatus? DocumentStatus { get; init; }
}
