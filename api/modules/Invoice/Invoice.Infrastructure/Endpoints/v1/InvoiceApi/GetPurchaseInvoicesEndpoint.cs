using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NexKoala.WebApi.Invoice.Application.Dtos;
using NexKoala.WebApi.Invoice.Domain.Entities.PO;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetPurchaseInvoicesEndpoint
{
    internal static RouteHandlerBuilder MapGetPurchaseInvoicesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/purchase-invoices",
                async (IInvoiceService service, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string invoiceNumber = null) =>
                {
                    var response = await service.GetPurchaseInvoices(page, pageSize, invoiceNumber);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetPurchaseInvoicesEndpoint))
            .WithSummary("Retrieves purchase invoices (self-billing)")
            .WithDescription("Retrieves purchase invoices (self-billing)")
            .Produces<PaginatedResult<PurchaseInvoiceHeader>>()
            .MapToApiVersion(1);
    }
}
