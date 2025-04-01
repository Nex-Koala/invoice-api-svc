using NexKoala.Framework.Core.Exceptions;

namespace NexKoala.WebApi.Invoice.Domain.Exceptions;
public sealed class ClassificationMappingNotFoundException : NotFoundException
{
    public ClassificationMappingNotFoundException(Guid id)
        : base($"ClassificationMapping with id {id} not found")
    {
    }
}
