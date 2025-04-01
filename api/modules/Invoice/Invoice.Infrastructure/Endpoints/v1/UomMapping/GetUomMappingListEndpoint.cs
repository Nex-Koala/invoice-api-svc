using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Paging;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.UomMappings.Get.v1;
using NexKoala.WebApi.Invoice.Application.Features.UomMappings.GetList.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.UomMapping;

public static class GetUomMappingListEndpoint
{
    internal static RouteHandlerBuilder MapGetUomMappingListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/",
                async (ISender mediator, [FromQuery] Guid userId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize) =>
                {
                    var response = await mediator.Send(new GetUomMappingList(userId, pageNumber ?? 1, pageSize ?? int.MaxValue));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetUomMappingListEndpoint))
            .WithSummary("Gets a list of uom mappings")
            .WithDescription("Gets a list of uom mappings with pagination and filtering support")
            .Produces<PagedList<UomMappingResponse>>()
            .RequirePermission("Permissions.UomMappings.View")
            .MapToApiVersion(1);
    }
}
