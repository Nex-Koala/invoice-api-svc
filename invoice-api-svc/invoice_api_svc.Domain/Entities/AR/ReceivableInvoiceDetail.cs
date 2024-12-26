namespace invoice_api_svc.Domain.Entities.AR
{
    /// <summary>
    /// Represents the Receivable Invoice Details.
    /// Maps to the "ARIBD" table.
    /// </summary>
    public class ReceivableInvoiceDetail
    {
        public decimal CNTBTCH { get; set; } // Batch Count
        public decimal CNTITEM { get; set; } // Item Count
        public decimal CNTLINE { get; set; } // Line Count
        public string IDINVC { get; set; } // Invoice Number
        public string IDITEM { get; set; } // Item Identifier
        public string TEXTDESC { get; set; } // Item Description
        public decimal QTYINVC { get; set; } // Quantity Invoiced
        public decimal UNITMEAS { get; set; } // Unit of Measurement
        public decimal AMTPRIC { get; set; } // Price Amount
        public decimal AMTEXTN { get; set; } // Extended Amount
        public decimal AMTTAX1 { get; set; } // Tax Amount
        public decimal AMTTAX2 { get; set; } // Tax Amount 2
        public decimal TOTTAX { get; set; } // Total Tax

        // Navigation property for the related Receivable Invoice Header
        public ReceivableInvoiceHeader ReceivableInvoiceHeader { get; set; }
    }
}
