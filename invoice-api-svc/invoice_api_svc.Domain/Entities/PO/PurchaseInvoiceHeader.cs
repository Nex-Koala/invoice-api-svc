using System;
using System.Collections.Generic;

namespace invoice_api_svc.Domain.Entities.PO
{
    public class PurchaseInvoiceHeader
    {
        public int PurchaseInvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string VendorName { get; set; }
        public ICollection<PurchaseInvoiceDetail> PurchaseInvoiceDetails { get; set; }
    }
}
