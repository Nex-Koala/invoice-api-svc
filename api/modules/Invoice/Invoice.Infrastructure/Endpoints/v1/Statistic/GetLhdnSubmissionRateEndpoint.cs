using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.Framework.Infrastructure.Identity.Users;
using NexKoala.WebApi.Invoice.Application.Features.Statistics.GetLhdnSubmissionRate.v1;
using NexKoala.WebApi.Invoice.Application.Features.Statistics.GetSageSubmissionRate.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Statistic;

public static class GetLhdnSubmissionRateEndpoint
{
    internal static RouteHandlerBuilder MapGetLhdnSubmissionRateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/lhdn/submission-rate",
                async (ISender mediator, HttpContext context, DateTime? startDate, DateTime? endDate) =>
                {
                    var userId = context.User.GetUserId();

                    if (string.IsNullOrEmpty(userId))
                        return Results.Unauthorized();

                    var request = new GetLhdnSubmissionRate(startDate, endDate);

                    if (!context.User.IsInRole("Admin"))
                    {
                        request = request with { UserId = userId };
                    }

                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetLhdnSubmissionRateEndpoint))
            .WithSummary("gets lhdn submission rate")
            .WithDescription("gets lhdn submission rate")
            .Produces<Response<SubmissionRateResponse>>()
            .RequirePermission("Permissions.InvoiceApi.View")
            .MapToApiVersion(1);
    }
}
