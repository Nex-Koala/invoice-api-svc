using invoice_api_svc.Domain.Common;

namespace invoice_api_svc.Domain.Entities
{
    public class ClassificationMapping : AuditableBaseEntity
    {
        public string LhdnClassificationCode { get; set; } = default!;
        public int ClassificationId { get; set; }
        public Classification Classification { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;
    }
}
