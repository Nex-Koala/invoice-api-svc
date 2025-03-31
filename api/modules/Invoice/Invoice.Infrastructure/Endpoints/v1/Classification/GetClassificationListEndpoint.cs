using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Classifications.GetList.v1;
using NexKoala.WebApi.Invoice.Application.Features.Classifications.Get.v1;
using NexKoala.Framework.Core.Paging;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Classification;

public static class GetClassificationListEndpoint
{
    internal static RouteHandlerBuilder MapGetClassificationListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/",
                async (ISender mediator, [FromQuery] Guid userId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize) =>
                {
                    var response = await mediator.Send(new GetClassificationList(userId, pageNumber ?? 1, pageSize ?? int.MaxValue));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetClassificationListEndpoint))
            .WithSummary("Gets a list of classifications")
            .WithDescription("Gets a list of classifications with pagination and filtering support")
            .Produces<PagedList<ClassificationResponse>>()
            .RequirePermission("Permissions.Classifications.View")
            .MapToApiVersion(1);
    }
}
