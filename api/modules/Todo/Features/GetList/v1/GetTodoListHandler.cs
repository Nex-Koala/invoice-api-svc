using NexKoala.Framework.Core.Paging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Specifications;
using NexKoala.WebApi.Todo.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace NexKoala.WebApi.Todo.Features.GetList.v1;

public sealed class GetTodoListHandler(
    [FromKeyedServices("todo")] IReadRepository<TodoItem> repository)
    : IRequestHandler<GetTodoListRequest, PagedList<TodoDto>>
{
    public async Task<PagedList<TodoDto>> Handle(GetTodoListRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new EntitiesByPaginationFilterSpec<TodoItem, TodoDto>(request.filter);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<TodoDto>(items, request.filter.PageNumber, request.filter.PageSize, totalCount);
    }
}
