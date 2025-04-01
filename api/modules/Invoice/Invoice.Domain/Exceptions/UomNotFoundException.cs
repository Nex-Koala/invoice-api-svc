using NexKoala.Framework.Core.Exceptions;

namespace NexKoala.WebApi.Invoice.Domain.Exceptions;
public sealed class UomNotFoundException : NotFoundException
{
    public UomNotFoundException(Guid id)
        : base($"Uom with id {id} not found")
    {
    }
}
