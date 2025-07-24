using DocumentFormat.OpenXml.InkML;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.Framework.Infrastructure.Identity.Users;
using NexKoala.WebApi.Invoice.Application.Features.Classifications.Create.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Classification;

public static class CreateClassificationEndpoint
{
    internal static RouteHandlerBuilder MapClassificationCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost(
                "/",
                async (HttpContext context, CreateClassificationCommand request, ISender mediator) =>
                {
                    var userId = context.User.GetUserId();
                    Guid.TryParse(userId, out Guid parsedUserId);
                    request = request with
                    {
                        UserId = parsedUserId
                    };
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(CreateClassificationEndpoint))
            .WithSummary("creates a classification")
            .WithDescription("creates a classification")
            .Produces<Response<Guid>>()
            .RequirePermission("Permissions.Classifications.Create")
            .MapToApiVersion(1);
    }
}
