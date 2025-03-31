using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Partners.GetList.v1;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;
using NexKoala.Framework.Core.Paging;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Partner;

public static class GetPartnerListEndpoint
{
    internal static RouteHandlerBuilder MapGetPartnerListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/",
                async (ISender mediator, [AsParameters] GetPartnerList request) =>
                {
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetPartnerListEndpoint))
            .WithSummary("Gets a list of partners")
            .WithDescription("Gets a list of partners with pagination and filtering support")
            .Produces<PagedList<PartnerResponse>>()
            .RequirePermission("Permissions.Partners.View")
            .MapToApiVersion(1);
    }
}
