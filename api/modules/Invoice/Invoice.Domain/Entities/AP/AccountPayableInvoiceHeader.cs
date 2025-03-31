using System.Collections.Generic;

namespace NexKoala.WebApi.Invoice.Domain.Entities.AP
{
    /// <summary>
    /// Represents the Accounts Payable Invoice Header.
    /// Maps to the "APIBH" table.
    /// </summary>
    public class AccountPayableInvoiceHeader
    {
        public string INVNUMBER { get; set; } // Invoice Number (SAGE: APIBH.IDINVC)
        public string SUPPLIERID { get; set; } // Supplier ID (SAGE: APIBH.IDVEND)
        public string SUPPLIERNAME { get; set; } // Supplier Name (Derived or stored elsewhere)
        public decimal INVDATE { get; set; } // Invoice Date (SAGE: APIBH.DATEINVC)
        public decimal INVAMOUNT { get; set; } // Invoice Total Amount (SAGE: APIBH.AMTINVCTOT)
        public string CURRENCY { get; set; } // Invoice Currency (SAGE: APIBH.CODECURN)
        public decimal EXCHANGERATE { get; set; } // Exchange Rate (SAGE: APIBH.EXCHRATEHC)

        // Navigation property for related Accounts Payable Invoice Details
        public ICollection<AccountPayableInvoiceDetail> AccountsPayableInvoiceDetails { get; set; } // Related details (SAGE: APIBD)
    }
}
