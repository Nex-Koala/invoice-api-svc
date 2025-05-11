using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NexKoala.Framework.Core.Persistence;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Settings;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRawDocument.v1;

public sealed class GetRawDocumentHandler(
    ILhdnApi lhdnApi,
    [FromKeyedServices("invoice:partners")] IRepository<Partner> partnerRepository,
    IOptions<EInvoiceSettings> options
) : IRequestHandler<GetRawDocument, Response<RawDocument>>
{
    public async Task<Response<RawDocument>> Handle(
        GetRawDocument request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        string partnerTin;

        if (request.IsAdmin ?? false)
        {
            partnerTin = options.Value.AdminTin;
        }
        else
        {
            var partner = await partnerRepository.FirstOrDefaultAsync(
                new PartnerByUserIdSpec(request.UserId),
                cancellationToken
            );

            if (partner == null)
            {
                return new Response<RawDocument>("Partner not found.");
            }

            if (string.IsNullOrWhiteSpace(partner.Tin))
            {
                return new Response<RawDocument>($"The TIN (Tax Identification Number) for {partner.Name} is not set.");
            }

            partnerTin = partner.Tin;
        }

        var item = await lhdnApi.GetDocumentAsync(request.Uuid, partnerTin);

        if (item == null)
        {
            throw new GenericException("Failed to get document.");
        }

        return new Response<RawDocument>(item);
    }
}
