namespace invoice_api_svc.Domain.Entities.OE
{
    /// <summary>
    /// Represents the Order Entry Detail.
    /// Maps to the "OEINVDD" table.
    /// </summary>
    public class OrderEntryDetail
    {
        public decimal INVUNIQ { get; set; } // Foreign Key to Header
        public int LINENUM { get; set; } // Line Number
        public string ITEM { get; set; } // Item Code
        public decimal QTY { get; set; } // Quantity
        public decimal UNITPRICE { get; set; } // Unit Price
        public decimal AMTTOTAL { get; set; } // Total Amount for the Line
        public string TAXCODE { get; set; } // Tax Code
        public decimal TAXAMOUNT { get; set; } // Tax Amount for the Line

        // Navigation Property
        public OrderEntryHeader OrderEntryHeader { get; set; }
    }
}
