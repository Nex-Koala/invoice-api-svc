using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.WebApi.Invoice.Application.Features.Codes.GetInvoiceTypes.v1;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetInvoiceTypesEndpoint
{
    internal static RouteHandlerBuilder MapGetInvoiceTypesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/invoice-types",
                async (ISender mediator) =>
                {
                    var response = await mediator.Send(new GetInvoiceTypes());
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetInvoiceTypesEndpoint))
            .WithSummary("get invoice types")
            .WithDescription("get invoice types")
            .Produces<Response<List<EInvoiceType>>>()
            .MapToApiVersion(1);
    }
}
