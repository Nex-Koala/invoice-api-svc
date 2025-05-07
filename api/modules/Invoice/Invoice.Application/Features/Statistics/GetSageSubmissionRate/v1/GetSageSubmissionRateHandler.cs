using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Statistics.GetSageSubmissionRate.v1;

public sealed class GetSageSubmissionRateHandler(
    [FromKeyedServices("invoice:invoiceDocuments")] IReadRepository<InvoiceDocument> repository
) : IRequestHandler<GetSageSubmissionRate, Response<List<SubmissionRateResponse>>>
{
    public async Task<Response<List<SubmissionRateResponse>>> Handle(
        GetSageSubmissionRate request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        Guid.TryParse(request.UserId, out var parsedUserId);
        var successSpec = new GetInvoiceDocumentBySubmissionStatusSpec(true, request.StartDate, request.EndDate, parsedUserId);
        var failedSpec = new GetInvoiceDocumentBySubmissionStatusSpec(false, request.StartDate, request.EndDate, parsedUserId);
        var successCount = await repository.CountAsync(successSpec, cancellationToken).ConfigureAwait(false);
        var failedCount = await repository.CountAsync(failedSpec, cancellationToken).ConfigureAwait(false);

        var data = new List<SubmissionRateResponse>
        {
            new SubmissionRateResponse { Label = "Success", Value = successCount },
            new SubmissionRateResponse { Label = "Failed", Value = failedCount }
        };

        return new Response<List<SubmissionRateResponse>>(data, "Submission rate retrieved successfully");
    }
}
