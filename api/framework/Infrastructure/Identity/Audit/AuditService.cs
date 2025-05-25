using NexKoala.Framework.Infrastructure.Identity.Persistence;
using Microsoft.EntityFrameworkCore;
using NexKoala.Framework.Core.Audit;
using NexKoala.Framework.Core.Wrappers;

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

    public async Task<PagedResponse<AuditTrailDto>> GetIdentityAuditTrailsAsync(
        int pageNumber,
        int pageSize,
        Guid? userId,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate)
    {
        var query = context.AuditTrails.AsQueryable();

        if (userId.HasValue)
        {
            query = query.Where(a => a.UserId == userId.Value);
        }

        if (startDate.HasValue)
        {
            query = query.Where(a => a.DateTime >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(a => a.DateTime <= endDate.Value);
        }

        var totalCount = await query.CountAsync();

        var trails = await (from a in query
                            join u in context.Users on a.UserId.ToString() equals u.Id into userGroup
                            from u in userGroup.DefaultIfEmpty()
                            orderby a.DateTime descending
                            select new AuditTrailDto
                            {
                                Id = a.Id,
                                UserId = a.UserId,
                                UserName = u.UserName,
                                Operation = a.Operation,
                                Entity = a.Entity,
                                DateTime = a.DateTime,
                                PreviousValues = a.PreviousValues,
                                NewValues = a.NewValues,
                                ModifiedProperties = a.ModifiedProperties,
                                PrimaryKey = a.PrimaryKey
                            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<AuditTrailDto>(trails, pageNumber, pageSize, totalCount);
    }
}
