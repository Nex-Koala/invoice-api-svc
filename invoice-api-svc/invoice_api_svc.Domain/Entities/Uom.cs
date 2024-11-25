using invoice_api_svc.Domain.Common;
using System;
using System.Collections.Generic;

namespace invoice_api_svc.Domain.Entities
{
    public class Uom : AuditableBaseEntity
    {
        public Guid UserId { get; set; } // Reference to Seller (optional if needed)
        public string Code { get; set; } = default!; // Unique Code for Seller UOM
        public string Description { get; set; } = default!; // Description of the UOM
        public bool IsDeleted { get; set; } = false;
        // Navigation Property for Mappings
        public ICollection<UomMapping> UomMappings { get; set; } = new List<UomMapping>();
    }
}
