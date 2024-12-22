namespace invoice_api_svc.Domain.Entities.PO
{
    /// <summary>
    /// Represents the Purchase Invoice Details for Self-Billing.
    /// Maps to the "POINVL" table.
    /// </summary>
    public class PurchaseInvoiceDetail
    {
        public string INVNUMBER { get; set; } // e-Invoice Code/Number (FK)
        public int LineNumber { get; set; } // Line Number
        public string ITEMDESC { get; set; } // Description of Product or Service
        public decimal UNITCOST { get; set; } // Unit Price
        public decimal TAXRATE1 { get; set; } // Tax Rate
        public decimal TAXMOUNT1 { get; set; } // Tax Amount
        public decimal TXEXPSAMT1 { get; set; } // Amount Exempted from Tax
        public decimal EXTENDED { get; set; } // Subtotal
        public decimal RQRECEIVED { get; set; } // Quantity
        public string RCPUNIT { get; set; } // Measurement
        public decimal DISCPCT { get; set; } // Discount Rate
        public decimal DISCOUNT { get; set; } // Discount Amount
    }
}
