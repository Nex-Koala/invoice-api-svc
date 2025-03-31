using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Get.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.ClassificationMapping;

public static class GetClassificationMappingEndpoint
{
    internal static RouteHandlerBuilder MapGetClassificationMappingEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/{id:guid}",
                async (Guid id, ISender mediator) =>
                {
                    var response = await mediator.Send(new GetClassificationMappingRequest(id));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetClassificationMappingEndpoint))
            .WithSummary("gets classification mapping by id")
            .WithDescription("gets classification mapping by id")
            .Produces<ClassificationMappingResponse>()
            .RequirePermission("Permissions.ClassificationMappings.View")
            .MapToApiVersion(1);
    }
}
