using System;
using System.Collections.Generic;

namespace NexKoala.WebApi.Invoice.Domain.Entities.AR
{
    /// <summary>
    /// Represents the Receivable Invoice Header.
    /// Maps to the "ARIBH" table.
    /// </summary>
    public class ReceivableInvoiceHeader
    {
        public decimal CNTBTCH { get; set; } // Batch Count
        public decimal CNTITEM { get; set; } // Item Count
        public string IDCUST { get; set; } // Customer ID
        public string IDINVC { get; set; } // Invoice Number
        public string CODECURN { get; set; } // Currency Code
        public decimal EXCHRATEHC { get; set; } // Exchange Rate
        public decimal AMTTAXTOT { get; set; } // Total Tax Amount
        public decimal AMTINVCTOT { get; set; } // Total Invoice Amount
        public decimal AMTNETTOT { get; set; } // Net Total Amount
        public decimal DATEINVC { get; set; } // Invoice Date
        public string INVCSTTS { get; set; } // Invoice Status
        public string INVCDESC { get; set; } // Invoice Description
        public string EMAIL { get; set; } // Customer Email
        public string CTACPHONE { get; set; } // Customer Contact Phone

        // Navigation property for related Receivable Invoice Details
        public ICollection<ReceivableInvoiceDetail> ReceivableInvoiceDetails { get; set; }
    }
}
