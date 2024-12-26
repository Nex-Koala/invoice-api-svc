using System.Text.Json.Serialization;

namespace invoice_api_svc.Domain.Entities.OE
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
        public decimal EXTICOST { get; set; } // Extended Cost
        public decimal EXTWEIGHT { get; set; } // Extended Weight
        public string TAXAUTH1 { get; set; } // Tax Authorization 1
        public decimal TAXAMOUNT1 { get; set; } // Tax Amount 1
        public decimal TAXRATE1 { get; set; } // Tax Rate 1
        public decimal TBASE1 { get; set; } // Tax Base Amount 1
        [JsonIgnore]
        public OrderCreditDebitHeader OrderCreditDebitHeader { get; set; }
    }
}
