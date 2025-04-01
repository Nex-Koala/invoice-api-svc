using NexKoala.Framework.Core.Exceptions;

namespace NexKoala.WebApi.Todo.Exceptions;
internal sealed class TodoItemNotFoundException : NotFoundException
{
    public TodoItemNotFoundException(Guid id)
        : base($"todo item with id {id} not found")
    {
    }
}
