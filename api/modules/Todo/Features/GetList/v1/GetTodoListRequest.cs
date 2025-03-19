using NexKoala.Framework.Core.Paging;
using MediatR;

namespace NexKoala.WebApi.Todo.Features.GetList.v1;
public record GetTodoListRequest(PaginationFilter filter) : IRequest<PagedList<TodoDto>>;
