using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.EInvoice.Invoice
{
    public class InvoiceItemRequest
    {
        public string Id { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public decimal TotItemVal { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }

        // New Tax Fields
        public decimal TaxAmount { get; set; }       // Total tax amount for the item
        public decimal TaxableAmount { get; set; }   // Taxable amount for the item
        public decimal TaxPercent { get; set; }      // Tax percentage applied
    }
}
