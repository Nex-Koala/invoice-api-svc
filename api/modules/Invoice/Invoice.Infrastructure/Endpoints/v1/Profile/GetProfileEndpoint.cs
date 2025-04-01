using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;
using NexKoala.WebApi.Invoice.Application.Features.Partners.GetByEmail.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Profile;

public static class GetProfileEndpoint
{
    internal static RouteHandlerBuilder MapGetProfileEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/",
                async ([FromQuery] string email, ISender mediator) =>
                {
                    var response = await mediator.Send(new GetPartnerByEmailRequest(email));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetProfileEndpoint))
            .WithSummary("gets profile details by email")
            .WithDescription("gets profile details by email")
            .Produces<Response<PartnerResponse>>()
            .RequirePermission("Permissions.Profile.View")
            .MapToApiVersion(1);
    }
}
