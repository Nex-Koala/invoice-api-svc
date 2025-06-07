using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NexKoala.WebApi.Invoice.Domain.Entities.OE;
using NexKoala.WebApi.Invoice.Application.Dtos;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetSalesInvoicesEndpoint
{
    internal static RouteHandlerBuilder MapGetSalesInvoicesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/sales-invoices",
                async (IInvoiceService service, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? invoiceNumber = null) =>
                {
                    var response = await service.GetSalesInvoices(page, pageSize, invoiceNumber);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetSalesInvoicesEndpoint))
            .WithSummary("Retrieves sales invoices from Order Entry tables.")
            .WithDescription("Retrieves sales invoices from Order Entry tables.")
            .Produces<PaginatedResult<OrderEntryHeader>>()
            .MapToApiVersion(1);
    }
}
