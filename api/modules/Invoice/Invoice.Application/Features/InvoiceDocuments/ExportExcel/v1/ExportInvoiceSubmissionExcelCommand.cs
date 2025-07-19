using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.ExportExcel.v1;
public record ExportInvoiceSubmissionExcelCommand : IRequest<Response<byte[]>>
{
    public string? Uuid { get; init; }
    public bool? Status { get; init; }
    public DateTimeOffset? IssueDateFrom { get; init; }
    public DateTimeOffset? IssueDateTo { get; init; }
    public string? UserId { get; init; }
    public List<string>? DocumentStatus { get; init; }
}
