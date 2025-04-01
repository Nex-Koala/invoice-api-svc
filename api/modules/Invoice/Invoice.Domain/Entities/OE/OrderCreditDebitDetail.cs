using System.Text.Json.Serialization;

namespace NexKoala.WebApi.Invoice.Domain.Entities.OE
{
    /// <summary>
    /// Represents the Order Credit/Debit Detail.
    /// Maps to the "OECRDD" table.
    /// DB no data
    /// </summary>
    public class OrderCreditDebitDetail
    {
        public decimal CRDUNIQ { get; set; } // Unique Identifier for Credit/Debit Note
        public int LINENUM { get; set; } // Line Number
        public string ITEM { get; set; } // Item Code
        public string DESC { get; set; } // Item Description
        public decimal QTYRETURN { get; set; } // Quantity Returned
        public decimal UNITPRICE { get; set; } // Unit Price
        public decimal TAMOUNT1 { get; set; } // Tax Amount 1
        public decimal TRATE1 { get; set; } // Tax Rate 1
        //public decimal TEAMOUNT1 { get; set; } // Amount Extempted from Tax
        public decimal EXTCRDMISC { get; set; } // Subtotal
        public string CRDUNIT { get; set; } // UOM

        [JsonIgnore]
        public OrderCreditDebitHeader OrderCreditDebitHeader { get; set; }
    }
}
