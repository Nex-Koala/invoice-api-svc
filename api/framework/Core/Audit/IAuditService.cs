using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.Framework.Core.Audit;
public interface IAuditService
{
    Task<List<AuditTrail>> GetUserTrailsAsync(Guid userId);
    Task<PagedResponse<AuditTrailDto>> GetIdentityAuditTrailsAsync(int pageNumber, int pageSize, Guid? userId, DateTimeOffset? startDate, DateTimeOffset? endDate);
}
