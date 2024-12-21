using System;
using System.Collections.Generic;

namespace invoice_api_svc.Domain.Entities.AR
{
    public class ReceivableInvoiceHeader
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public ICollection<ReceivableInvoiceDetail> InvoiceDetails { get; set; }
    }
}
