using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Codes.GetCurrencyCodes.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetCurrencyCodesEndpoint
{
    internal static RouteHandlerBuilder MapGetCurrencyCodesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/currency-codes",
                async (ISender mediator) =>
                {
                    var response = await mediator.Send(new GetCurrencyCodes());
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetCurrencyCodesEndpoint))
            .WithSummary("get currency codes")
            .WithDescription("get currency codes")
            .Produces<Response<List<CurrencyCode>>>()
            .MapToApiVersion(1);
    }
}
