using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Classifications.Get.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Classification;

public static class GetClassificationEndpoint
{
    internal static RouteHandlerBuilder MapGetClassificationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/{id:guid}",
                async (Guid id, ISender mediator) =>
                {
                    var response = await mediator.Send(new GetClassificationRequest(id));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetClassificationEndpoint))
            .WithSummary("gets classification by id")
            .WithDescription("gets classification by id")
            .Produces<ClassificationResponse>()
            .RequirePermission("Permissions.Classifications.View")
            .MapToApiVersion(1);
    }
}
