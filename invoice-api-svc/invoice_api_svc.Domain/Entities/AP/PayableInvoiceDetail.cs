namespace invoice_api_svc.Domain.Entities.AP
{
    /// <summary>
    /// Represents the Accounts Payable Invoice Detail.
    /// Maps to the "APIBD" table.
    /// </summary>
    public class PayableInvoiceDetail
    {
        public decimal CNTBTCH { get; set; } // Batch Identifier
        public decimal CNTITEM { get; set; } // Item Identifier
        public decimal CNTLINE { get; set; } // Line Number
        public string IDGLACCT { get; set; } // General Ledger Account Code
        public decimal AMTDIST { get; set; } // Distribution Amount
        public decimal AMTTOTTAX { get; set; } // Total Tax Amount
        public string ACCTCODE { get; set; } // Maps to ACCTCODE
    }
}
