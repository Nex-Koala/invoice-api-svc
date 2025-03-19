using System.Collections.ObjectModel;
using NexKoala.Framework.Core.Audit;
using MediatR;

namespace NexKoala.Framework.Infrastructure.Identity.Audit;
public class AuditPublishedEvent : INotification
{
    public AuditPublishedEvent(Collection<AuditTrail>? trails)
    {
        Trails = trails;
    }
    public Collection<AuditTrail>? Trails { get; }
}
