﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.Framework.Infrastructure.Identity.Users;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GenerateInvoice.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GenerateInvoiceEndpoint
{
    internal static RouteHandlerBuilder MapGenerateInvoiceEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "{uuid}/generate-invoice",
                async (string uuid, ISender mediator, HttpContext context) =>
                {
                    if (context.User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
                    {
                        return Results.BadRequest();
                    }

                    bool isAdmin = false;
                    if (context.User.IsInRole("Admin"))
                    {
                        isAdmin = true;
                    }

                    var response = await mediator.Send(new GenerateInvoiceCommand(uuid, userId, isAdmin));
                    return Results.File(response.Data, "application/pdf", $"invoice_{uuid}.pdf");
                }
            )
            .WithName(nameof(GenerateInvoiceEndpoint))
            .WithSummary("Generate an invoice")
            .WithDescription("Generates a PDF invoice for the given UUID.")
            .Produces<FileResult>()
            .RequirePermission("Permissions.InvoiceApi.Create")
            .MapToApiVersion(1);
    }
}
