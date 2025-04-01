using System.Collections.ObjectModel;
using MediatR;
using NexKoala.Framework.Core.Audit;

namespace NexKoala.Framework.Infrastructure.Identity.Audit;
public class AuditPublishedEvent : INotification
{
    public AuditPublishedEvent(Collection<AuditTrail>? trails)
    {
        Trails = trails;
    }
    public Collection<AuditTrail>? Trails { get; }
}
