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
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;
public static class GetInvoiceDocumentEndpoint
{
    internal static RouteHandlerBuilder MapGetInvoiceDocumentEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/invoice-document/{uuid}",
                async (ISender mediator, string uuid, HttpContext context) =>
                {
                    var response = await mediator.Send(new GetInvoiceDocument(uuid));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetInvoiceDocumentEndpoint))
            .WithSummary("get invoice document by uuid")
            .WithDescription("get invoice document by uuid")
            //.Produces<Response<InvoiceDocumentResponse>>()
            .RequirePermission("Permissions.InvoiceApi.View")
            .MapToApiVersion(1);
    }
}
