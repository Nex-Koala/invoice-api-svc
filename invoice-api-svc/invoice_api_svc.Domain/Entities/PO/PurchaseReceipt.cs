using System;

namespace invoice_api_svc.Domain.Entities.PO
{
    public class PurchaseReceipt
    {
        public int ReceiptId { get; set; }
        public int PurchaseOrderId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public PurchaseOrderHeader PurchaseOrderHeader { get; set; }
    }
}
