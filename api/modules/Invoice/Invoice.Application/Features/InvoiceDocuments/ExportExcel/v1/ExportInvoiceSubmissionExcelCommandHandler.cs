using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Settings;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.ExportExcel.v1;
public class ExportInvoiceSubmissionExcelCommandHandler(
    ILogger<ExportInvoiceSubmissionExcelCommandHandler> logger,
    IOptions<EInvoiceSettings> options,
    IInvoiceService invoiceService,
    [FromKeyedServices("invoice:invoiceDocuments")] IRepository<InvoiceDocument> invoiceDocumentRepository
) : IRequestHandler<ExportInvoiceSubmissionExcelCommand, Response<byte[]>>
{
    public async Task<Response<byte[]>> Handle(ExportInvoiceSubmissionExcelCommand request, CancellationToken cancellationToken)
    {
        Guid.TryParse(request.UserId, out var parsedUserId);
        var spec = new InvoiceDocumentListFilterWithoutPaginationSpec(
            request.Uuid,
            true,
            request.DocumentStatus,
            request.IssueDateFrom,
            request.IssueDateTo,
            parsedUserId
        );

        var documents = await invoiceDocumentRepository.ListAsync(spec, cancellationToken).ConfigureAwait(false);

        var excelBytes = invoiceService.ExportInvoiceSubmissionExcel(documents);

        return new Response<byte[]>(excelBytes);
    }
}
