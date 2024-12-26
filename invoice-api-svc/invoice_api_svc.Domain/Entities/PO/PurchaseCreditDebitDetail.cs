namespace invoice_api_svc.Domain.Entities.PO
{
    /// <summary>
    /// Represents the Self-Billing Credit/Debit Note Detail.
    /// Maps to the "POCRNL" table in SAGE.
    /// </summary>
    public class PurchaseCreditDebitNoteDetail
    {
        public decimal CRNHSEQ { get; set; } // Header Sequence Number
        public decimal CRNLREV { get; set; } // Header Sequence Number
        public int CRNLSEQ { get; set; } // Line Sequence Number
        public string ITEMNO { get; set; } // Item Number
        public string ITEMDESC { get; set; } // Item Description
        public string RETUNIT { get; set; } // Return Unit
        public decimal RETCONV { get; set; } // Unit Conversion
        public decimal RQRETURNED { get; set; } // Quantity Returned
        public decimal UNITCOST { get; set; } // Unit Cost
        public decimal EXTENDED { get; set; } // Extended Amount
        public decimal TAXRATE1 { get; set; } // Tax Rate
        public decimal TAXAMOUNT1 { get; set; } // Tax Amount
        public decimal TXEXPSAMT1 { get; set; } // Exempted Tax Amount
        public PurchaseCreditDebitNoteHeader PurchaseCreditDebitNoteHeader { get; set; }
    }
}
