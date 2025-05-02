using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Domain.Events;
public class InvoiceAuditPublishedEvent : INotification
{
    public InvoiceAuditPublishedEvent(Collection<AuditTrail>? trails)
    {
        Trails = trails;
    }
    public Collection<AuditTrail>? Trails { get; }
}
