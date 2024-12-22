using System;

namespace invoice_api_svc.Domain.Entities.PO
{
    /// <summary>
    /// Represents the Purchase Invoice Header.
    /// Maps to the "POINVH1" table.
    /// </summary>
    public class PurchaseInvoiceHeader
    {
        public string INVNUMBER { get; set; } // e-Invoice Code/Number
        public DateTime DATE { get; set; } // e-Invoice Date
        public TimeSpan AUDTTIME { get; set; } // e-Invoice Time
        public string VDNAME { get; set; } // Supplier's Name
        public string VDEMAIL { get; set; } // Supplier's Email
        public string VDADDRESS1 { get; set; } // Supplier's Address Line 1
        public string VDADDRESS2 { get; set; } // Supplier's Address Line 2
        public string VDADDRESS3 { get; set; } // Supplier's Address Line 3
        public string VDADDRESS4 { get; set; } // Supplier's Address Line 4
        public string VDPHONE { get; set; } // Supplier's Contact Number
        public string CURRENCY { get; set; } // Invoice Currency Code
        public decimal RATE { get; set; } // Currency Exchange Rate
        public decimal EXTENDED { get; set; } // Total Excluding Tax
        public decimal DOCTOTAL { get; set; } // Total Including Tax
        public decimal SCAMOUNT { get; set; } // Total Net Amount
        public decimal TERMDUEAMT { get; set; } // Total Payable Amount
    }
}
