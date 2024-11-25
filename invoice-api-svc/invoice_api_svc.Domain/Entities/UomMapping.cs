using invoice_api_svc.Domain.Common;

namespace invoice_api_svc.Domain.Entities
{
    public class UomMapping : AuditableBaseEntity
    {
        public string LhdnUomCode { get; set; } = default!;
        public int UomId { get; set; }
        public Uom Uom { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;
    }
}
