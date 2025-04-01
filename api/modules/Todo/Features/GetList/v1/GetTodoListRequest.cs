using MediatR;
using NexKoala.Framework.Core.Paging;

namespace NexKoala.WebApi.Todo.Features.GetList.v1;
public record GetTodoListRequest(PaginationFilter filter) : IRequest<PagedList<TodoDto>>;
