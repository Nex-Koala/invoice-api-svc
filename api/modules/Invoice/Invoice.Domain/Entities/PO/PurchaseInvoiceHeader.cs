using System;
using System.Collections.Generic;

namespace NexKoala.WebApi.Invoice.Domain.Entities.PO
{
    /// <summary>
    /// Represents the Purchase Invoice Header.
    /// Maps to the "POINVH1" table in SAGE.
    /// </summary>
    public class PurchaseInvoiceHeader
    {
        public decimal INVHSEQ { get; set; } // Invoice Header Sequence
        public string VDNAME { get; set; } // Supplier Name
        public string VDEMAIL { get; set; } // Supplier Email
        public string VDADDRESS1 { get; set; } // Supplier Address Line 1
        public string VDADDRESS2 { get; set; } // Supplier Address Line 2
        public string VDADDRESS3 { get; set; } // Supplier Address Line 3
        public string VDADDRESS4 { get; set; } // Supplier Address Line 4
        public string VDPHONE { get; set; } // Supplier Phone
        public string INVNUMBER { get; set; } // Invoice Number
        public decimal DATE { get; set; } // Invoice Date
        public decimal AUDTTIME { get; set; } // Audit Time
        public string CURRENCY { get; set; } // Invoice Currency Code
        public decimal RATE { get; set; } // Currency Exchange Rate
        public decimal EXTENDED { get; set; } // Total Excluding Tax
        public decimal DOCTOTAL { get; set; } // Total Including Tax
        public decimal SCAMOUNT { get; set; } // Total Payable Amount
        public string TAXGROUP { get; set; } // Tax Group
        public ICollection<PurchaseInvoiceDetail> PurchaseInvoiceDetails { get; set; }
    }
}
