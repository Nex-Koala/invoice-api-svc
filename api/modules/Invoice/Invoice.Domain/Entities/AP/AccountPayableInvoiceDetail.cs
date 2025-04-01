namespace NexKoala.WebApi.Invoice.Domain.Entities.AP
{
    /// <summary>
    /// Represents the Accounts Payable Invoice Detail.
    /// Maps to the "APIBD" table.
    /// </summary>
    public class AccountPayableInvoiceDetail
    {
        public string INVNUMBER { get; set; } // Invoice Number (SAGE: APIBD.IDDIST)
        public int LINENUMBER { get; set; } // Line Number (SAGE: APIBD.CNTLINE)
        public string ITEMDESC { get; set; } // Item Description (SAGE: APIBD.TEXTDESC)
        public decimal QTY { get; set; } // Quantity (SAGE: APIBD.QTYINVC)
        public decimal UNITPRICE { get; set; } // Unit Price (SAGE: APIBD.BILLRATE)
        public decimal TOTAL { get; set; } // Total Amount (SAGE: APIBD.AMTDIST)

        // Navigation property for the related Accounts Payable Invoice Header
        public AccountPayableInvoiceHeader AccountsPayableInvoiceHeader { get; set; } // Related header (SAGE: APIBH)
    }
}
