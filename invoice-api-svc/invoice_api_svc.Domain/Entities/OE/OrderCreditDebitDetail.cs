namespace invoice_api_svc.Domain.Entities.OE
{
    /// <summary>
    /// Represents the Order Credit/Debit Detail.
    /// Maps to the "OECRDD" table.
    /// </summary>
    public class OrderCreditDebitDetail
    {
        public decimal CRDUNIQ { get; set; } // Reference to Header
        public int LINENUM { get; set; } // Line Number
        public string ITEM { get; set; } // Item Code
        public decimal QTY { get; set; } // Quantity
        public decimal UNITPRICE { get; set; } // Unit Price
        public decimal LINETOTAL { get; set; } // Line Total Amount
        public decimal TAXAMOUNT { get; set; } // Tax on Line
        public string UOM { get; set; } // Unit of Measure
        public string ORDNUMBER { get; set; } // Maps to ORDNUMBER
        public string ITEMID { get; set; } // Maps to ITEMID
    }
}
