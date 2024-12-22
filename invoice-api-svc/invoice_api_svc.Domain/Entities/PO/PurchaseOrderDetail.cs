namespace invoice_api_svc.Domain.Entities.PO
{
    /// <summary>
    /// Represents the Purchase Order Detail.
    /// Maps to the "POPORD" table.
    /// </summary>
    public class PurchaseOrderDetail
    {
        public string POPORID { get; set; } // Foreign Key to Purchase Order Header
        public int LINENUM { get; set; } // Line Number
        public string ITEMID { get; set; } // Item Code
        public int QUANTITY { get; set; } // Quantity Ordered
        public decimal UNITPRICE { get; set; } // Unit Price
    }
}
