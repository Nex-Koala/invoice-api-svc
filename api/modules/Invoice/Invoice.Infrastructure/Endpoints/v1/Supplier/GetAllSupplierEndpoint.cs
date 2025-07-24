using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.Framework.Infrastructure.Identity.Users;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
using NexKoala.WebApi.Invoice.Application.Features.Suppliers.GetAll.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Supplier;
public static class GetAllSupplierEndpoint
{
    internal static RouteHandlerBuilder MapGetAllSupplierEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/",
                async (ISender mediator, HttpContext context) =>
                {
                    var userId = context.User.GetUserId();
                    Guid.TryParse(userId, out Guid parsedUserId);

                    var response = await mediator.Send(new GetAllSupplier(parsedUserId));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetAllSupplierEndpoint))
            .WithSummary("Gets a list of suppliers")
            .WithDescription("Gets a list of suppliers")
            .Produces<Response<SupplierDto>>()
            .RequirePermission("Permissions.InvoiceApi.View")
            .MapToApiVersion(1);
    }
}
