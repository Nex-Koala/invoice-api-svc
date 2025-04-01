using NexKoala.Framework.Core.Exceptions;

namespace NexKoala.WebApi.Invoice.Domain.Exceptions;
public sealed class UomMappingNotFoundException : NotFoundException
{
    public UomMappingNotFoundException(Guid id)
        : base($"UomMapping with id {id} not found")
    {
    }
}
