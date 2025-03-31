using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Create.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.ClassificationMapping;

public static class CreateClassificationMappingEndpoint
{
    internal static RouteHandlerBuilder MapClassificationMappingCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost(
                "/",
                async (CreateClassificationMappingCommand request, ISender mediator) =>
                {
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(CreateClassificationMappingEndpoint))
            .WithSummary("creates a classification mapping")
            .WithDescription("creates a classification mapping")
            .Produces<Response<Guid>>()
            .RequirePermission("Permissions.ClassificationMappings.Create")
            .MapToApiVersion(1);
    }
}
