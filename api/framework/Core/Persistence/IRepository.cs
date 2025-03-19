using Ardalis.Specification;
using NexKoala.Framework.Core.Domain.Contracts;

namespace NexKoala.Framework.Core.Persistence;
public interface IRepository<T> : IRepositoryBase<T>
    where T : class, IAggregateRoot
{
}

public interface IReadRepository<T> : IReadRepositoryBase<T>
    where T : class, IAggregateRoot
{
}
