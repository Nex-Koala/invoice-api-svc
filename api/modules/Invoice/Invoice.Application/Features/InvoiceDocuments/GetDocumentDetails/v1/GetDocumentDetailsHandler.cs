using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetDocumentDetails.v1;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Settings;

public sealed class GetDocumentDetailsHandler(
    ILhdnApi lhdnApi,
    [FromKeyedServices("invoice:partners")] IRepository<Partner> partnerRepository,
    IOptions<EInvoiceSettings> options
) : IRequestHandler<GetDocumentDetails, Response<DocumentDetails>>
{
    public async Task<Response<DocumentDetails>> Handle(
        GetDocumentDetails request,
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
                return new Response<DocumentDetails>("Partner not found.");
            }

            if (string.IsNullOrWhiteSpace(partner.Tin))
            {
                return new Response<DocumentDetails>($"The TIN (Tax Identification Number) for {partner.Name} is not set.");
            }

            partnerTin = partner.Tin;
        }

        var item = await lhdnApi.GetDocumentDetailsAsync(request.Uuid, partnerTin);

        if (item == null)
        {
            throw new GenericException("Failed to get document details.");
        }

        return new Response<DocumentDetails>(item);
    }
}
