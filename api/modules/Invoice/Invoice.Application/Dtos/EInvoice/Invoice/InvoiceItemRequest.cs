using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Invoice
{
    public class InvoiceItemRequest
    {
        public string Id { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public decimal Subtotal { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string ClassificationCode { get; set; } // Classification code for the item

        // New Tax Fields
        public decimal TaxAmount { get; set; }       // Total tax amount for the item
        public decimal TaxableAmount { get; set; }   // Taxable amount for the item
        public decimal TaxPercent { get; set; }      // Tax percentage applied
    }
}
