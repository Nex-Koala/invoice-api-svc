using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Update.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Partner;

public static class UpdatePartnerEndpoint
{
    internal static RouteHandlerBuilder MapPartnerUpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut(
                "/{id:guid}",
                async (Guid id, UpdatePartnerCommand request, ISender mediator) =>
                {
                    if (id != request.Id)
                        return Results.BadRequest();
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(UpdatePartnerEndpoint))
            .WithSummary("update a partner")
            .WithDescription("update a partner")
            .Produces<UpdatePartnerResponse>()
            .RequirePermission("Permissions.Partners.Update")
            .MapToApiVersion(1);
    }
}
