using NexKoala.Framework.Core.Exceptions;

namespace NexKoala.WebApi.Invoice.Domain.Exceptions;
public sealed class PartnerNotFoundException : NotFoundException
{
    public PartnerNotFoundException(Guid id)
        : base($"Partner with id {id} not found")
    {
    }

    public PartnerNotFoundException(string email)
        : base($"Partner with email {email} not found")
    {
    }
}
