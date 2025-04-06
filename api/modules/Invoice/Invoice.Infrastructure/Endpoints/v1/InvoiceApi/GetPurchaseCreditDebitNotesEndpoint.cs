using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NexKoala.WebApi.Invoice.Application.Dtos;
using NexKoala.WebApi.Invoice.Domain.Entities.PO;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetPurchaseCreditDebitNotesEndpoint
{
    internal static RouteHandlerBuilder MapGetPurchaseCreditDebitNotesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/purchase-credit-debit-notes",
                async (IInvoiceService service, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? noteNumber = null) =>
                {
                    var response = await service.GetPurchaseCreditDebitNotes(page, pageSize, noteNumber);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetPurchaseCreditDebitNotesEndpoint))
            .WithSummary("Retrieves sales invoices from Order Entry tables.")
            .WithDescription("Retrieves sales invoices from Order Entry tables.")
            .Produces<PaginatedResult<PurchaseCreditDebitNoteHeader>>()
            .MapToApiVersion(1);
    }
}
