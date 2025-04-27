using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.RecentDocument;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
using NexKoala.Framework.Core.Persistence;
using NexKoala.WebApi.Invoice.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRecentDocuments.v1;

public sealed class GetRecentDocumentsHandler(
    ILhdnApi lhdnApi,
    [FromKeyedServices("invoice:partners")]IRepository<Partner> partnerRepository
) : IRequestHandler<GetRecentDocuments, Response<RecentDocuments>>
{
    public async Task<Response<RecentDocuments>> Handle(
        GetRecentDocuments request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        // get user TIN
        var partner = await partnerRepository.FirstOrDefaultAsync(new PartnerByUserIdSpec(request.UserId), cancellationToken);

        if (partner == null)
        {
            return new Response<RecentDocuments>("Partner not found.");
        }

        if (string.IsNullOrWhiteSpace(partner.Tin))
        {
            return new Response<RecentDocuments>($"The TIN (Tax Identification Number) for {partner.Name} is not set.");
        }

        string partnerTin = partner!.Tin;
        var item = await lhdnApi.GetRecentDocumentsAsync(request, partnerTin);

        if (item == null)
        {
            throw new GenericException("Failed to get document.");
        }

        return new Response<RecentDocuments>(item);
    }
}
