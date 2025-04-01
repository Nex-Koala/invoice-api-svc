using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Codes.GetUnitTypes.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetUnitTypesEndpoint
{
    internal static RouteHandlerBuilder MapGetUnitTypesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/unit-types",
                async (ISender mediator) =>
                {
                    var response = await mediator.Send(new GetUnitTypes());
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetUnitTypesEndpoint))
            .WithSummary("get unit types")
            .WithDescription("get unit types")
            .Produces<Response<List<UnitType>>>()
            .MapToApiVersion(1);
    }
}
