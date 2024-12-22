using System;
using System.Collections.Generic;

namespace invoice_api_svc.Domain.Entities.OE
{
    /// <summary>
    /// Represents the Order Entry Header.
    /// Maps to the "OEINVH" table.
    /// </summary>
    public class OrderEntryHeader
    {
        public string ORDERID { get; set; } // Order ID
        public string CUSTOMERID { get; set; } // Customer ID
        public DateTime ORDERDATE { get; set; } // Order Date
        public decimal TOTALAMOUNT { get; set; } // Total Order Amount
    }
}
