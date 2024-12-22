using System;

namespace invoice_api_svc.Domain.Entities.PO
{
    /// <summary>
    /// Represents the Purchase Order Header.
    /// Maps to the "POPORH" table.
    /// </summary>
    public class PurchaseOrderHeader
    {
        public string POPORID { get; set; } // Unique Purchase Order Identifier
        public string SUPPLIER { get; set; } // Supplier Identifier
        public DateTime ORDERDATE { get; set; } // Order Date
        public decimal TOTALAMOUNT { get; set; } // Total Purchase Order Amount
        public string CURRENCY { get; set; } // Currency Code
    }
}
