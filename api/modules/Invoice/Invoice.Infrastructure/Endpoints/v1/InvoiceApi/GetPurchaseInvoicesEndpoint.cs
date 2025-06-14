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
                async (IInvoiceService service, [AsParameters] InvoiceFilterParams filter) =>
                {
                    var response = await service.GetPurchaseInvoices(filter);
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
