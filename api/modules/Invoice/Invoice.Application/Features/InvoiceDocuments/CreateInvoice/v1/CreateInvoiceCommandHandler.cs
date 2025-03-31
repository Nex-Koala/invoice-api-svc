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
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.CreateInvoice.v1;

public sealed class CreateInvoiceCommandHandler(
    [FromKeyedServices("invoice:invoiceDocuments")] IRepository<InvoiceDocument> repository
) : IRequestHandler<CreateInvoiceCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        CreateInvoiceCommand request,
        CancellationToken cancellationToken
    )
    {
        // Map command to domain entity
        var invoice = request.Adapt<InvoiceDocument>();

        // Save invoice using repository
        await repository.AddAsync(invoice, cancellationToken);

        // Return the invoice Id wrapped in a Response
        return new Response<Guid>(invoice.Id);
    }
}
