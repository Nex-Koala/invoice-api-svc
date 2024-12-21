using System;
using System.Collections.Generic;

namespace invoice_api_svc.Domain.Entities.PO
{
    public class PurchaseOrderHeader
    {
        public int PurchaseOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string VendorName { get; set; }
        public ICollection<PurchaseReceipt> PurchaseReceipts { get; set; }
    }
}
