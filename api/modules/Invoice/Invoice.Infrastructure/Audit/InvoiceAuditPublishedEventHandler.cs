using MediatR;
using Microsoft.Extensions.Logging;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Events;
using NexKoala.WebApi.Invoice.Infrastructure.Persistence;

namespace NexKoala.WebApi.Invoice.Infrastructure.Audit;
public class InvoiceAuditPublishedEventHandler(ILogger<InvoiceAuditPublishedEventHandler> logger, InvoiceDbContext context) : INotificationHandler<InvoiceAuditPublishedEvent>
{
    public async Task Handle(InvoiceAuditPublishedEvent notification, CancellationToken cancellationToken)
    {
        if (context == null) return;
        logger.LogInformation("received invoice audit trails");
        try
        {
            await context.Set<AuditTrail>().AddRangeAsync(notification.Trails!, default);
        }
        catch
        {
            logger.LogError("error while saving invoice audit trail");
        }
        return;
    }
}
