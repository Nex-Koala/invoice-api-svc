using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.EInvoice
{
    public class InvoiceDocumentRequest
    {
        // Invoice metadata
        public string InvoiceNumber { get; set; }  // "JSON-INV12345"
        public DateTime IssueDate { get; set; }    // "2024-07-23"
        public string DocumentCurrencyCode { get; set; }  // "MYR"
        public string TaxCurrencyCode { get; set; }  // "MYR"
        public decimal TotalAmount { get; set; }    // Total invoice amount
        public decimal TaxAmount { get; set; }      // Total tax amount

        // Supplier details
        public string SupplierName { get; set; }    // Supplier's Name
        public string SupplierTIN { get; set; }     // Supplier's TIN
        public string SupplierBRN { get; set; }     // Supplier's BRN
        public string SupplierAddress { get; set; } // Supplier's Address
        public string SupplierCity { get; set; }    // Supplier's City
        public string SupplierPostalCode { get; set; } // Supplier's Postal Code
        public string SupplierCountryCode { get; set; } // Supplier's Country Code

        // Customer details
        public string CustomerName { get; set; }    // Customer's Name
        public string CustomerTIN { get; set; }     // Customer's TIN
        public string CustomerBRN { get; set; }     // Customer's BRN
        public string CustomerAddress { get; set; } // Customer's Address
        public string CustomerCity { get; set; }    // Customer's City
        public string CustomerPostalCode { get; set; } // Customer's Postal Code
        public string CustomerCountryCode { get; set; } // Customer's Country Code

        // Invoice line items
        public List<InvoiceLineDto> InvoiceLines { get; set; }  // List of Invoice Line Items
    }

    public class InvoiceLineDto
    {
        public string LineNumber { get; set; }      // "1234"
        public decimal Quantity { get; set; }       // Quantity invoiced
        public decimal LineAmount { get; set; }     // Line extension amount
        public string Description { get; set; }     // Item description
        public string UnitCode { get; set; }        // "C62" - Unit code
        public string CurrencyCode { get; set; }    // Currency code (MYR)
    }
}
