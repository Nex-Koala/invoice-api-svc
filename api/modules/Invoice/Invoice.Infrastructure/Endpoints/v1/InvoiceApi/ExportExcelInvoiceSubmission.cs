using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.Framework.Infrastructure.Identity.Users;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.ExportExcel.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;
public static class ExportInvoiceSubmissionExcelEndpoint
{
    internal static RouteHandlerBuilder MapExportInvoiceSubmissionExcelEndpoint(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints
            .MapGet(
                "/export-submission-history",
                async (
                    ISender mediator,
                    HttpContext context,
                    [AsParameters] ExportInvoiceSubmissionExcelCommand request
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
                    return Results.File(response.Data, "application/octet-stream", "invoice_submission_history.xlsx");
                }
            )
            .WithName(nameof(ExportInvoiceSubmissionExcelEndpoint))
            .WithSummary("export excel for invoice submission")
            .WithDescription("export excel for invoice submission")
            .RequirePermission("Permissions.InvoiceApi.View")
            .MapToApiVersion(1);
    }
}
