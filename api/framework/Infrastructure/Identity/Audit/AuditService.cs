﻿using NexKoala.Framework.Core.Audit;
using NexKoala.Framework.Infrastructure.Identity.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NexKoala.Framework.Infrastructure.Identity.Audit;
public class AuditService(IdentityDbContext context) : IAuditService
{
    public async Task<List<AuditTrail>> GetUserTrailsAsync(Guid userId)
    {
        var trails = await context.AuditTrails
           .Where(a => a.UserId == userId)
           .OrderByDescending(a => a.DateTime)
           .Take(250)
           .ToListAsync();
        return trails;
    }
}
