using System;

namespace invoice_api_svc.Domain.Entities.OE
{
    /// <summary>
    /// Represents the Order Credit/Debit Header.
    /// Maps to the "OECRDH" table.
    /// </summary>
    public class OrderCreditDebitHeader
    {
        public decimal CRDUNIQ { get; set; } // Unique Identifier
        public string ORDERID { get; set; } // Order Identifier
        public string CUSTOMER { get; set; } // Customer Identifier
        public decimal AMOUNT { get; set; } // Amount
        public string CURRENCY { get; set; } // Currency
        public DateTime TRANSDATE { get; set; } // Transaction Date
        public string ORDNUMBER { get; set; } // Order Identifier
    }
}
