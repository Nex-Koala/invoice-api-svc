using invoice_api_svc.Domain.Common;
using System;
using System.Collections.Generic;

namespace invoice_api_svc.Domain.Entities
{
    public class Classification : AuditableBaseEntity
    {
        public Guid UserId { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;
        public ICollection<ClassificationMapping> ClassificationMappings { get; set; } = new List<ClassificationMapping>();
    }
}
