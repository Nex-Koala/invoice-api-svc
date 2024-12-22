namespace invoice_api_svc.Domain.Entities.AP
{
    /// <summary>
    /// Represents the Accounts Payable Invoice Header.
    /// Maps to the "APIBH" table.
    /// </summary>
    public class PayableInvoiceHeader
    {
        public decimal CNTBTCH { get; set; } // Batch Identifier
        public decimal CNTITEM { get; set; } // Item Identifier
        public string IDVEND { get; set; } // Vendor Identifier
        public decimal DATEINVC { get; set; } // Invoice Date
        public decimal AMTTOTAL { get; set; } // Total Amount
        public string CODECURN { get; set; } // Currency Code
        public string DESCRIPTION { get; set; } // Description or Remarks
        public string INVCNUM { get; set; } // Maps to INVCNUM
    }
}
