using NexKoala.Framework.Core.Exceptions;

namespace NexKoala.WebApi.Invoice.Domain.Exceptions;
public sealed class ClassificationNotFoundException : NotFoundException
{
    public ClassificationNotFoundException(Guid id)
        : base($"Classification with id {id} not found")
    {
    }
}
