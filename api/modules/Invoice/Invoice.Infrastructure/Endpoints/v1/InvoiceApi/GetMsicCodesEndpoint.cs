using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Codes.GetMsicCodes.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetMsicCodesEndpoint
{
    internal static RouteHandlerBuilder MapGetMsicCodesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/msic-codes",
                async (ISender mediator) =>
                {
                    var response = await mediator.Send(new GetMsicCodes());
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetMsicCodesEndpoint))
            .WithSummary("get msic codes")
            .WithDescription("get msic codes")
            .Produces<Response<List<MsicSubCategoryCode>>>()
            .MapToApiVersion(1);
    }
}
