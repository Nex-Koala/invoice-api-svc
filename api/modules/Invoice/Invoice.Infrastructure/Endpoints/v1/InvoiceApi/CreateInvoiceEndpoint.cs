using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.CreateInvoice.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class CreateInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapCreateInvoiceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost(
                "/create-invoice",
                async ([FromBody] CreateInvoiceCommand request, ISender mediator) =>
                {
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(CreateInvoiceEndpoint))
            .WithSummary("create invoice")
            .WithDescription("create invoice")
            .Produces<object>()
            .RequirePermission("Permissions.InvoiceApi.Create")
            .MapToApiVersion(1);
    }
}
