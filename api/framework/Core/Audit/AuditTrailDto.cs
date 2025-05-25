using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.Framework.Core.Audit;

public class AuditTrailDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public string? Operation { get; set; }
    public string? Entity { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public string? PreviousValues { get; set; }
    public string? NewValues { get; set; }
    public string? ModifiedProperties { get; set; }
    public string? PrimaryKey { get; set; }
}
