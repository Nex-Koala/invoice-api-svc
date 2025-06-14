using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NexKoala.WebApi.Invoice.Domain.Entities.OE;
using NexKoala.WebApi.Invoice.Application.Dtos;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetCreditDebitNotesEndpoint
{
    internal static RouteHandlerBuilder MapGetCreditDebitNotesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/credit-debit-notes",
                async (IInvoiceService service, [AsParameters] InvoiceFilterParams filter) =>
                {
                    var response = await service.GetCreditDebitNotes(filter);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetCreditDebitNotesEndpoint))
            .WithSummary("Retrieves credit/debit notes from Order Entry tables")
            .WithDescription("Retrieves credit/debit notes from Order Entry tables")
            .Produces<PaginatedResult<OrderCreditDebitHeader>>()
            .MapToApiVersion(1);
    }
}
