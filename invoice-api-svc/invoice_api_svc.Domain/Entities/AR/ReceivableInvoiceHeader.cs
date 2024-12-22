using System;

namespace invoice_api_svc.Domain.Entities.AR
{
    /// <summary>
    /// Represents the Receivable Invoice Header.
    /// Maps to the "ARIBH" table.
    /// </summary>
    public class ReceivableInvoiceHeader
    {
        public string INVNUMBER { get; set; } // Invoice Number
        public string CUSTOMERID { get; set; } // Customer ID
        public DateTime INVDATE { get; set; } // Invoice Date
        public string CURRENCY { get; set; } // Currency
        public decimal RATE { get; set; } // Exchange Rate
    }
}
