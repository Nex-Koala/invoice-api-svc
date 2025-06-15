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
                async (IInvoiceService service, [AsParameters] InvoiceFilterParams filter) =>
                {
                    var response = await service.GetSalesInvoices(filter);
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
