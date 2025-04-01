using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Delete.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Partner;

public static class DeletePartnerEndpoint
{
    internal static RouteHandlerBuilder MapPartnerDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete(
                "/{id:guid}",
                async (Guid id, ISender mediator) =>
                {
                    var response = await mediator.Send(new DeletePartnerCommand(id));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(DeletePartnerEndpoint))
            .WithSummary("deletes partner by id")
            .WithDescription("deletes partner by id")
            .Produces<Response<Guid>>()
            .RequirePermission("Permissions.Partners.Delete")
            .MapToApiVersion(1);
    }
}
