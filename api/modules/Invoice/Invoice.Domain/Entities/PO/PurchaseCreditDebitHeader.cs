using System;
using System.Collections.Generic;

namespace NexKoala.WebApi.Invoice.Domain.Entities.PO
{
    /// <summary>
    /// Represents the Self-Billing Credit/Debit Note Header.
    /// Maps to the "POCRNH1" table in SAGE.
    /// </summary>
    public class PurchaseCreditDebitNoteHeader
    {
        public decimal CRNHSEQ { get; set; } // Header Sequence Number
        public string VDCODE { get; set; } // Supplier Code
        public string VDNAME { get; set; } // Supplier Name
        public string VDEMAIL { get; set; } // Supplier Email
        public string VDADDRESS1 { get; set; } // Supplier Address Line 1
        public string VDADDRESS2 { get; set; } // Supplier Address Line 2
        public string VDADDRESS3 { get; set; } // Supplier Address Line 3
        public string VDADDRESS4 { get; set; } // Supplier Address Line 4
        public string VDCITY { get; set; }
        public string VDSTATE { get; set; }
        public string VDZIP { get; set; }
        public string VDCOUNTRY { get; set; }
        public string VDPHONE { get; set; } // Supplier Phone
        public string CRNNUMBER { get; set; } // Credit Note Number
        public string INVNUMBER { get; set; } // Invoice Number
        public decimal DATE { get; set; } // Document Date
        public decimal AUDTTIME { get; set; } // Audit Time
        public string CURRENCY { get; set; } // Currency Code
        public decimal RATE { get; set; } // Currency Exchange Rate
        public decimal EXTENDED { get; set; } // Total Excluding Tax
        public decimal DOCTOTAL { get; set; } // Total Including Tax
        public decimal SCAMOUNT { get; set; } // Total Payable Amount
        public string SupplierTIN { get; set; } = "EI00000000020";
        public string SupplierBRN { get; set; } // Supplier BRN
        public ICollection<PurchaseCreditDebitNoteDetail> PurchaseCreditDebitNoteDetails { get; set; }
    }
}
