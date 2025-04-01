using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Partner;

public static class GetPartnerEndpoint
{
    internal static RouteHandlerBuilder MapGetPartnerEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/{id:guid}",
                async (Guid id, ISender mediator) =>
                {
                    var response = await mediator.Send(new GetPartnerRequest(id));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetPartnerEndpoint))
            .WithSummary("gets partner by id")
            .WithDescription("gets partner by id")
            .Produces<PartnerResponse>()
            .RequirePermission("Permissions.Partners.View")
            .MapToApiVersion(1);
    }
}
