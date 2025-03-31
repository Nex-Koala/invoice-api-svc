using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Paging;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Uoms.Get.v1;
using NexKoala.WebApi.Invoice.Application.Features.Uoms.GetList.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Uom;

public static class GetUomListEndpoint
{
    internal static RouteHandlerBuilder MapGetUomListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/",
                async (ISender mediator, [FromQuery] Guid userId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize) =>
                {
                    var response = await mediator.Send(new GetUomList(userId, pageNumber ?? 1, pageSize ?? int.MaxValue));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetUomListEndpoint))
            .WithSummary("Gets a list of uoms")
            .WithDescription("Gets a list of uoms with pagination and filtering support")
            .Produces<PagedList<UomResponse>>()
            .RequirePermission("Permissions.Uoms.View")
            .MapToApiVersion(1);
    }
}
