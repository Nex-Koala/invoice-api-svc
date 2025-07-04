﻿using NexKoala.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Paging;

namespace NexKoala.WebApi.Todo.Features.GetList.v1;

public static class GetTodoListEndpoint
{
    internal static RouteHandlerBuilder MapGetTodoListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/search", async (ISender mediator, [FromBody] PaginationFilter filter) =>
        {
            var response = await mediator.Send(new GetTodoListRequest(filter));
            return Results.Ok(response);
        })
        .WithName(nameof(GetTodoListEndpoint))
        .WithSummary("Gets a list of todo items with paging support")
        .WithDescription("Gets a list of todo items with paging support")
        .Produces<PagedList<TodoDto>>()
        .RequirePermission("Permissions.Todos.View")
        .MapToApiVersion(1);
    }
}
