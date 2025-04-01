using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.GetList.v1;
using NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Get.v1;
using NexKoala.Framework.Core.Paging;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.ClassificationMapping;

public static class GetClassificationMappingListEndpoint
{
    internal static RouteHandlerBuilder MapGetClassificationMappingListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/",
                async (ISender mediator, [FromQuery] Guid userId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize) =>
                {
                    var response = await mediator.Send(new GetClassificationMappingList(userId, pageNumber ?? 1, pageSize ?? int.MaxValue));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetClassificationMappingListEndpoint))
            .WithSummary("Gets a list of classification mappings")
            .WithDescription("Gets a list of classification mappings with pagination and filtering support")
            .Produces<PagedList<ClassificationMappingResponse>>()
            .RequirePermission("Permissions.ClassificationMappings.View")
            .MapToApiVersion(1);
    }
}
