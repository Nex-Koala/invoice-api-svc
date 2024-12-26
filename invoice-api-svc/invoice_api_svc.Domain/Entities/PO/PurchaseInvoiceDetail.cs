namespace invoice_api_svc.Domain.Entities.PO
{
    /// <summary>
    /// Represents the Purchase Invoice Details for Self-Billing.
    /// Maps to the "POINVL" table.
    /// </summary>
    public class PurchaseInvoiceDetail
    {
        public decimal INVHSEQ { get; set; } // Invoice Header Sequence
        public decimal INVLREV { get; set; } // Invoice Line Revision
        public string ITEMDESC { get; set; } // Item Description
        public decimal UNITCOST { get; set; } // Unit Cost
        public decimal EXTENDED { get; set; } // Total Amount
        public decimal RQRECEIVED { get; set; } // Quantity Received
        public string RCPUNIT { get; set; } // Measurement Unit
        public decimal TAXRATE1 { get; set; } // Tax Rate
        public decimal TAXAMOUNT1 { get; set; } // Tax Amount
        public decimal TXEXPSAMT1 { get; set; } // Exempted Tax Amount
        public decimal DISCPCT { get; set; } // Discount Rate
        public decimal DISCOUNT { get; set; } // Discount Amount
        public PurchaseInvoiceHeader PurchaseInvoiceHeader { get; set; }

    }
}
