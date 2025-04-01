using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Update.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.ClassificationMapping;

public static class UpdateClassificationMappingEndpoint
{
    internal static RouteHandlerBuilder MapClassificationMappingUpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut(
                "/{id:guid}",
                async (Guid id, UpdateClassificationMappingCommand request, ISender mediator) =>
                {
                    if (id != request.Id)
                        return Results.BadRequest();
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(UpdateClassificationMappingEndpoint))
            .WithSummary("update a classification mapping")
            .WithDescription("update a classification mapping")
            .Produces<UpdateClassificationMappingResponse>()
            .RequirePermission("Permissions.ClassificationMappings.Update")
            .MapToApiVersion(1);
    }
}
