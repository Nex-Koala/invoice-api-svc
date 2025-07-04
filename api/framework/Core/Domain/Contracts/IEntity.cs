﻿using System.Collections.ObjectModel;
using NexKoala.Framework.Core.Domain.Events;

namespace NexKoala.Framework.Core.Domain.Contracts;

public interface IEntity
{
    Collection<DomainEvent> DomainEvents { get; }
}

public interface IEntity<out TId> : IEntity
{
    TId Id { get; }
}
