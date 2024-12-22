using System;

namespace invoice_api_svc.Domain.Entities.AR
{
    /// <summary>
    /// Represents the Receivables Reference.
    /// Maps to the "ARGLREF" table.
    /// </summary>
    public class ReceivablesReference
    {
        public decimal SOURCE { get; set; } // Source Identifier
        public decimal GLDEST { get; set; } // General Ledger Destination
        public DateTime AUDTDATE { get; set; } // Audit Date
        public string AUDTUSER { get; set; } // Audit User
        public string SEGMENT1 { get; set; } // GL Segment 1
        public string SEGMENT2 { get; set; } // GL Segment 2
        public decimal BASETAX { get; set; } // Base Tax Amount
        public decimal TAXAMOUNT { get; set; } // Tax Amount
    }
}
