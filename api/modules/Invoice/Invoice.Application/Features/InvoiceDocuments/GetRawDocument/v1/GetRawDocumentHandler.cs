using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRawDocument.v1;

public sealed class GetRawDocumentHandler(
    ILhdnApi lhdnApi,
    [FromKeyedServices("invoice:partners")] IRepository<Partner> partnerRepository
) : IRequestHandler<GetRawDocument, Response<RawDocument>>
{
    public async Task<Response<RawDocument>> Handle(
        GetRawDocument request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        // get user TIN
        var partner = await partnerRepository.FirstOrDefaultAsync(new PartnerByUserIdSpec(request.UserId), cancellationToken);
        string partnerTin = partner!.Tin;
        if (partnerTin == null)
        {
            return new Response<RawDocument>($"The TIN (Tax Identification Number) for {partner.Name} is not set.");
        }

        var item = await lhdnApi.GetDocumentAsync(request.Uuid, partnerTin);

        if (item == null)
            throw new GenericException("Failed to get document.");

        return new Response<RawDocument>(item);
    }
}
