﻿using NexKoala.Framework.Infrastructure.Identity.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Audit;

namespace NexKoala.Framework.Infrastructure.Identity.Audit;
public class AuditPublishedEventHandler(ILogger<AuditPublishedEventHandler> logger, IdentityDbContext context) : INotificationHandler<AuditPublishedEvent>
{
    public async Task Handle(AuditPublishedEvent notification, CancellationToken cancellationToken)
    {
        if (context == null) return;
        logger.LogInformation("received audit trails");
        try
        {
            await context.Set<AuditTrail>().AddRangeAsync(notification.Trails!, default);
            await context.SaveChangesAsync(default);
        }
        catch
        {
            logger.LogError("error while saving audit trail");
        }
        return;
    }
}
