using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using NexKoala.Framework.Core.Domain.Contracts;
using NexKoala.Framework.Core.Domain.Events;

namespace NexKoala.Framework.Core.Domain;

public abstract class BaseEntity<TId> : IEntity<TId>
{
    public TId Id { get; protected init; } = default!;
    [NotMapped]
    public Collection<DomainEvent> DomainEvents { get; } = new Collection<DomainEvent>();
    public void QueueDomainEvent(DomainEvent @event)
    {
        if (!DomainEvents.Contains(@event))
            DomainEvents.Add(@event);
    }
}

public abstract class BaseEntity : BaseEntity<Guid>
{
    protected BaseEntity() => Id = Guid.NewGuid();
}
