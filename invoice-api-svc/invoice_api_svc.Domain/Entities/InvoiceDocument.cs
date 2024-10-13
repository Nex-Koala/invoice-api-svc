using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace invoice_api_svc.Domain.Entities
{
    public class InvoiceDocument
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; } // "JSON-INV12345"

        [Required]
        public DateTime IssueDate { get; set; } // "2024-07-23"

        [Required]
        [StringLength(3)]
        public string DocumentCurrencyCode { get; set; } // "MYR"

        [StringLength(3)]
        public string TaxCurrencyCode { get; set; } // "MYR"

        public decimal TotalAmount { get; set; } // Total Payable Amount

        public decimal TaxAmount { get; set; } // Tax Total

        public Supplier Supplier { get; set; } // Supplier Info

        public Customer Customer { get; set; } // Customer Info

        public ICollection<InvoiceLine> InvoiceLines { get; set; } // Invoice Line Items
    }

}
