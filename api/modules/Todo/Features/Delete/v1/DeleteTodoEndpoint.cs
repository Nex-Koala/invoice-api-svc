﻿using Asp.Versioning;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace NexKoala.WebApi.Todo.Features.Delete.v1;
public static class DeleteTodoEndpoint
{
    internal static RouteHandlerBuilder MapTodoItemDeletionEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{id:guid}", async (Guid id, ISender mediator) =>
            {
                await mediator.Send(new DeleteTodoCommand(id));
                return Results.NoContent();
            })
            .WithName(nameof(DeleteTodoEndpoint))
            .WithSummary("Deletes a todo item")
            .WithDescription("Deleted a todo item")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Todos.Delete")
            .MapToApiVersion(new ApiVersion(1, 0));

    }
}
