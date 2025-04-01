using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Codes.GetClassificationCodes.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetClassificationCodesEndpoint
{
    internal static RouteHandlerBuilder MapGetClassificationCodesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/classification-codes",
                async (ISender mediator) =>
                {
                    var response = await mediator.Send(new GetClassificationCodes());
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetClassificationCodesEndpoint))
            .WithSummary("get classification codes")
            .WithDescription("get classification codes")
            .Produces<Response<List<ClassificationCode>>>()
            .MapToApiVersion(1);
    }
}
