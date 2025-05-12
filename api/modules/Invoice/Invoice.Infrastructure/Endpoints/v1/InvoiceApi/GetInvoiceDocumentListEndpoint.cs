using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.Framework.Infrastructure.Identity.Users;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetList.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetInvoiceDocumentListEndpoint
{
    internal static RouteHandlerBuilder MapGetInvoiceDocumentListEndpoint(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints
            .MapGet(
                "/invoice-documents",
                async (
                    ISender mediator,
                    HttpContext context,
                    [AsParameters] GetInvoiceDocumentList request
                ) =>
                {
                    var userId = context.User.GetUserId();

                    if (string.IsNullOrEmpty(userId))
                        return Results.Unauthorized();

                    if (!context.User.IsInRole("Admin"))
                    {
                        request = request with { UserId = userId };
                    }

                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetInvoiceDocumentListEndpoint))
            .WithSummary("get invoice document list")
            .WithDescription("get invoice document list")
            //.Produces<PagedResponse<InvoiceDocumentResponse>>()
            .RequirePermission("Permissions.InvoiceApi.View")
            .MapToApiVersion(1);
    }
}
