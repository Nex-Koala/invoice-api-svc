using System.ComponentModel.DataAnnotations;

namespace invoice_api_svc.Domain.Entities
{
    public class InvoiceLine
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string LineNumber { get; set; } // "1234"

        public decimal Quantity { get; set; } // Invoiced Quantity

        public decimal LineAmount { get; set; } // LineExtensionAmount

        [StringLength(100)]
        public string Description { get; set; } // Item description

        [StringLength(10)]
        public string UnitCode { get; set; } // "C62" - Unit code for invoiced quantity

        [StringLength(3)]
        public string CurrencyCode { get; set; } // "MYR"

        public int InvoiceDocumentId { get; set; }
        public InvoiceDocument InvoiceDocument { get; set; }
    }

}
