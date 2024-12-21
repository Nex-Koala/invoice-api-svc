using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Domain.Entities.PO
{
    public class PurchaseInvoiceDetail
    {
        public int PurchaseInvoiceDetailId { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public PurchaseInvoiceHeader PurchaseInvoiceHeader { get; set; }
    }
}
