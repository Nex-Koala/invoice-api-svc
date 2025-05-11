using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Codes.GetStateCodes.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetStateCodesEndpoint
{
    internal static RouteHandlerBuilder MapGetStateCodesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/state-codes",
                async (ISender mediator) =>
                {
                    var response = await mediator.Send(new GetStateCodes());
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetStateCodesEndpoint))
            .WithSummary("get state codes")
            .WithDescription("get state codes")
            .Produces<Response<List<StateCode>>>()
            .MapToApiVersion(1);
    }
}
