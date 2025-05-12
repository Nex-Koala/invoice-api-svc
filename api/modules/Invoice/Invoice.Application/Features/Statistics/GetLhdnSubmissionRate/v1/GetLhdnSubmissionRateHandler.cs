using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications;
using NexKoala.WebApi.Invoice.Application.Features.Statistics.GetSageSubmissionRate.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Statistics.GetLhdnSubmissionRate.v1;

public sealed class GetLhdnSubmissionRateHandler(
    [FromKeyedServices("invoice:invoiceDocuments")] IReadRepository<InvoiceDocument> repository
) : IRequestHandler<GetLhdnSubmissionRate, Response<List<SubmissionRateResponse>>>
{
    public async Task<Response<List<SubmissionRateResponse>>> Handle(
        GetLhdnSubmissionRate request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        Guid.TryParse(request.UserId, out var parsedUserId);
        var submittedSpec = new GetInvoiceDocumentByDocumentStatusSpec(DocumentStatus.Submitted, request.StartDate, request.EndDate, parsedUserId);
        var validSpec = new GetInvoiceDocumentByDocumentStatusSpec(DocumentStatus.Valid, request.StartDate, request.EndDate, parsedUserId);
        var invalidSpec = new GetInvoiceDocumentByDocumentStatusSpec(DocumentStatus.Invalid, request.StartDate, request.EndDate, parsedUserId);
        var cancelledSpec = new GetInvoiceDocumentByDocumentStatusSpec(DocumentStatus.Cancelled, request.StartDate, request.EndDate, parsedUserId);

        var submittedCount = await repository.CountAsync(submittedSpec, cancellationToken).ConfigureAwait(false);
        var validCount = await repository.CountAsync(validSpec, cancellationToken).ConfigureAwait(false);
        var invalidCount = await repository.CountAsync(invalidSpec, cancellationToken).ConfigureAwait(false);
        var cancelledCount = await repository.CountAsync(cancelledSpec, cancellationToken).ConfigureAwait(false);

        var data = new List<SubmissionRateResponse>
        {
            new SubmissionRateResponse { Label = "Submitted", Value = submittedCount },
            new SubmissionRateResponse { Label = "Valid", Value = validCount },
            new SubmissionRateResponse { Label = "Invalid", Value = invalidCount },
            new SubmissionRateResponse { Label = "Cancelled", Value = cancelledCount },
        };

        return new Response<List<SubmissionRateResponse>>(data, "Submission rate retrieved successfully");
    }
}
