using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Create.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Partner;

public static class CreatePartnerEndpoint
{
    internal static RouteHandlerBuilder MapPartnerCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost(
                "/",
                async (CreatePartnerCommand request, ISender mediator) =>
                {
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(CreatePartnerEndpoint))
            .WithSummary("creates a partner")
            .WithDescription("creates a partner")
            .Produces<Response<Guid>>()
            .RequirePermission("Permissions.Partners.Create")
            .MapToApiVersion(1);
    }
}
