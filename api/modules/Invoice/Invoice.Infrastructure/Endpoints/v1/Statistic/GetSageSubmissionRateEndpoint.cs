using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Statistics.GetSageSubmissionRate.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Statistic;

public static class GetSageSubmissionRateEndpoint
{
    internal static RouteHandlerBuilder MapGetSageSubmissionRateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/sage/submission-rate",
                async (ISender mediator, DateTime? startDate, DateTime? endDate) =>
                {
                    var response = await mediator.Send(new GetSageSubmissionRate(startDate, endDate));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetSageSubmissionRateEndpoint))
            .WithSummary("gets sage submission rate")
            .WithDescription("gets sage submission rate")
            .Produces<Response<SubmissionRateResponse>>()
            .RequirePermission("Permissions.InvoiceApi.View")
            .MapToApiVersion(1);
    }
}
