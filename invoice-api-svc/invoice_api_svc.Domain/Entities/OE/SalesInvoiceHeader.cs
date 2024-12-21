using System;
using System.Collections.Generic;

namespace invoice_api_svc.Domain.Entities.OE
{
    public class SalesInvoiceHeader
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public ICollection<SalesInvoiceDetail> InvoiceDetails { get; set; }
    }
}
