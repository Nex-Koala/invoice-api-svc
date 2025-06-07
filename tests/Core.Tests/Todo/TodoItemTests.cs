using NexKoala.WebApi.Todo.Domain;
using NexKoala.WebApi.Todo.Domain.Events;
using Xunit;

namespace NexKoala.Framework.Core.Tests.Todo;

public class TodoItemTests
{
    [Fact]
    public void Create_SetsPropertiesAndQueuesDomainEvent()
    {
        var item = TodoItem.Create("title", "note");

        Assert.Equal("title", item.Title);
        Assert.Equal("note", item.Note);
        var domainEvent = Assert.Single(item.DomainEvents);
        Assert.IsType<TodoItemCreated>(domainEvent);
        var created = (TodoItemCreated)domainEvent;
        Assert.Equal(item.Id, created.Id);
        Assert.Equal("title", created.Title);
        Assert.Equal("note", created.Note);
    }

    [Fact]
    public void Update_ChangesValuesAndQueuesDomainEvent()
    {
        var item = TodoItem.Create("old", "old note");
        item.DomainEvents.Clear();

        item.Update("new", "new note");

        Assert.Equal("new", item.Title);
        Assert.Equal("new note", item.Note);
        var domainEvent = Assert.Single(item.DomainEvents);
        Assert.IsType<TodoItemUpdated>(domainEvent);
        var updated = (TodoItemUpdated)domainEvent;
        Assert.Equal(item, updated.item);
    }
}
