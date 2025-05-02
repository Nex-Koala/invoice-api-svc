using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Infrastructure.Persistence;

namespace NexKoala.WebApi.Invoice.Infrastructure.Audit;
public class AuditService(InvoiceDbContext context) : IAuditService
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
