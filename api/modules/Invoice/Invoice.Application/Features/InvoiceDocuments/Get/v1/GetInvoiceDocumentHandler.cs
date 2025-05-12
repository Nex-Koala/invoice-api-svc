using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
public sealed class GetInvoiceDocumentHandler(
    [FromKeyedServices("invoice:invoiceDocuments")] IRepository<InvoiceDocument> repository
    ) : IRequestHandler<GetInvoiceDocument, Response<InvoiceDocumentResponse>>
{
    public async Task<Response<InvoiceDocumentResponse>> Handle(GetInvoiceDocument request, CancellationToken cancellationToken)
    {
        var spec = new GetInvoiceDocumentResponseByUuid(request.Uuid);
        var invoiceDocument = await repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (invoiceDocument == null)
            throw new InvoiceDocumentNotFoundException(request.Uuid);

        return new Response<InvoiceDocumentResponse>(invoiceDocument);
    }
}
