using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Classifications.Update.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Classification;

public static class UpdateClassificationEndpoint
{
    internal static RouteHandlerBuilder MapClassificationUpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut(
                "/{id:guid}",
                async (Guid id, UpdateClassificationCommand request, ISender mediator) =>
                {
                    if (id != request.Id)
                        return Results.BadRequest();
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(UpdateClassificationEndpoint))
            .WithSummary("update a classification")
            .WithDescription("update a classification")
            .Produces<UpdateClassificationResponse>()
            .RequirePermission("Permissions.Classifications.Update")
            .MapToApiVersion(1);
    }
}
