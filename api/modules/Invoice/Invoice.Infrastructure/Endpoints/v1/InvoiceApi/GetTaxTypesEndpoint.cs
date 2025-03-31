using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Codes.GetTaxTypes.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetTaxTypesEndpoint
{
    internal static RouteHandlerBuilder MapGetTaxTypesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/tax-types",
                async (ISender mediator) =>
                {
                    var response = await mediator.Send(new GetTaxTypes());
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetTaxTypesEndpoint))
            .WithSummary("get tax types")
            .WithDescription("get tax types")
            .Produces<Response<List<TaxType>>>()
            .MapToApiVersion(1);
    }
}
