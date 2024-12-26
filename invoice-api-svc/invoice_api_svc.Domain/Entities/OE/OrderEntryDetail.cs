using System.Text.Json.Serialization;

namespace invoice_api_svc.Domain.Entities.OE
{
    /// <summary>
    /// Represents the Order Entry Detail.
    /// Maps to the "OEINVD" table.
    /// </summary>
    public class OrderEntryDetail
    {
        public string DESC { get; set; } // Description of Product or Service
        public decimal UNITPRICE { get; set; } // Unit Price
        public decimal TRATE1 { get; set; } // Tax Rate
        public decimal TAMOUNT1 { get; set; } // Tax Amount
        public decimal EXTINVMISC { get; set; } // Subtotal
        public decimal QTYSHIPPED { get; set; } // Quantity
        public string INVUNIT { get; set; } // Measurement
        public decimal DISCPER { get; set; } // Discount Rate 
        public decimal INVDISC { get; set; } // Discount Amount
        public decimal INVUNIQ { get; set; } // Order ID
        [JsonIgnore]
        public OrderEntryHeader OrderEntryHeader { get; set; }
    }
}
