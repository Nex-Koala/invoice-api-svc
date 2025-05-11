using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.Framework.Infrastructure.Identity.Users;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.SubmitInvoice.v1;
using NexKoala.WebApi.Invoice.Application.Interfaces;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class SubmitInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapSubmitInvoiceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost(
                "/submit-invoice",
                async ([FromBody] SubmitInvoiceCommand request, ISender mediator, HttpContext context, IQuotaService quotaService) =>
                {
                    if (context.User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
                    {
                        return Results.Unauthorized();
                    }

                    if (!await quotaService.TryAcquireQuota(userId))
                    {
                        List<string> errors = new();
                        errors.Add("Quota exceeded");
                        return Results.BadRequest(new Response<object>
                        {
                            Errors = errors,
                            Message = $"You've reached your max submissions.",
                            Succeeded = false
                        });
                    }

                    request.UserId = userId;
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(SubmitInvoiceEndpoint))
            .WithSummary("submit invoice")
            .WithDescription("submit invoice")
            .Produces<object>()
            .RequirePermission("Permissions.InvoiceApi.Create")
            .MapToApiVersion(1);
    }
}
