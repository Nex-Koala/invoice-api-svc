using System;
using System.Collections.Generic;

namespace invoice_api_svc.Domain.Entities.AP
{
    public class PayableInvoiceHeader
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string VendorName { get; set; }
        public ICollection<PayableInvoiceDetail> InvoiceDetails { get; set; }
    }
}
