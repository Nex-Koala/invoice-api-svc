using NexKoala.Framework.Core.Domain.Events;

namespace NexKoala.WebApi.Catalog.Domain.Events;
public sealed record ProductCreated : DomainEvent
{
    public Product? Product { get; set; }
}
