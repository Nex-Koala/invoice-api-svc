﻿using Carter;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Infrastructure.Persistence;
using NexKoala.WebApi.Todo.Domain;
using NexKoala.WebApi.Todo.Features.Create.v1;
using NexKoala.WebApi.Todo.Features.Delete.v1;
using NexKoala.WebApi.Todo.Features.Get.v1;
using NexKoala.WebApi.Todo.Features.GetList.v1;
using NexKoala.WebApi.Todo.Features.Update.v1;
using NexKoala.WebApi.Todo.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace NexKoala.WebApi.Todo;
public static class TodoModule
{

    public class Endpoints : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var todoGroup = app.MapGroup("todos").WithTags("todos");
            todoGroup.MapTodoItemCreationEndpoint();
            todoGroup.MapGetTodoEndpoint();
            todoGroup.MapGetTodoListEndpoint();
            todoGroup.MapTodoItemUpdationEndpoint();
            todoGroup.MapTodoItemDeletionEndpoint();
        }
    }
    public static WebApplicationBuilder RegisterTodoServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<TodoDbContext>();
        builder.Services.AddScoped<IDbInitializer, TodoDbInitializer>();
        builder.Services.AddKeyedScoped<IRepository<TodoItem>, TodoRepository<TodoItem>>("todo");
        builder.Services.AddKeyedScoped<IReadRepository<TodoItem>, TodoRepository<TodoItem>>("todo");
        return builder;
    }
    public static WebApplication UseTodoModule(this WebApplication app)
    {
        return app;
    }
}
