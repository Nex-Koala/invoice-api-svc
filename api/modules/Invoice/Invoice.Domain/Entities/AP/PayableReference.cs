using System;

namespace NexKoala.WebApi.Invoice.Domain.Entities.AP
{
    /// <summary>
    /// Represents the Payable Reference.
    /// Maps to the "APGLREF" table.
    /// </summary>
    public class PayableReference
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
