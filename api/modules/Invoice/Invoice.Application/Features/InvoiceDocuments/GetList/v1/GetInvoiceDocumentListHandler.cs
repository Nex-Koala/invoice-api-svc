using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetList.v1;
public class GetInvoiceDocumentListHandler(
    [FromKeyedServices("invoice:invoiceDocuments")] IRepository<InvoiceDocument> repository
    ) : IRequestHandler<GetInvoiceDocumentList, PagedResponse<InvoiceDocumentResponse>>
{
    public async Task<PagedResponse<InvoiceDocumentResponse>> Handle(GetInvoiceDocumentList request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        Guid.TryParse(request.UserId, out var parsedUserId);

        var spec = new InvoiceDocumentListFilterSpec(
            request.PageNumber,
            request.PageSize,
            request.Uuid,
            request.Status,
            request.DocumentStatus,
            request.IssueDateFrom,
            request.IssueDateTo,
            parsedUserId
        );
        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);

        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);
        return new PagedResponse<InvoiceDocumentResponse>(
            items,
            request.PageNumber,
            request.PageSize,
            totalCount
        );
    }
}
