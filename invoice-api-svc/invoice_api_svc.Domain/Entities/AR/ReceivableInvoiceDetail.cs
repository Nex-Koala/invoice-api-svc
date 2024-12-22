namespace invoice_api_svc.Domain.Entities.AR
{
    /// <summary>
    /// Represents the Receivable Invoice Details.
    /// Maps to the "ARIBD" table.
    /// </summary>
    public class ReceivableInvoiceDetail
    {
        public string INVNUMBER { get; set; } // Invoice Number (FK)
        public int LINENUMBER { get; set; } // Line Number
        public string ITEMDESC { get; set; } // Item Description
        public decimal UNITCOST { get; set; } // Unit Cost
        public decimal TAXAMOUNT { get; set; } // Tax Amount
        public decimal TOTALAMOUNT { get; set; } // Total Amount
    }
}
