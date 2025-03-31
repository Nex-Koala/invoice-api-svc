using System;
using System.Collections.Generic;

namespace NexKoala.WebApi.Invoice.Domain.Entities.OE
{
    /// <summary>
    /// Represents the Order Entry Header.
    /// Maps to the "OEINVH" table.
    /// </summary>
    public class OrderEntryHeader
    {
        public string BILNAME { get; set; } // Buyer Name
        public string BILEMAIL { get; set; } // Buyer Email
        public string BILADDR1 { get; set; } // Buyer Address 
        public string BILADDR2 { get; set; } // Buyer Address 
        public string BILADDR3 { get; set; } // Buyer Address 
        public string BILADDR4 { get; set; } // Buyer Address 
        public string BILSTATE { get; set; } // Buyer State 
        public string BILZIP { get; set; } // Buyer Zip
        public string BILCOUNTRY { get; set; } // Buyer Country
        public string BILPHONE { get; set; } // Buyer Contact
        public string INVNUMBER { get; set; } // e-Invoice Code/ Number
        public decimal INVUNIQ { get; set; } // Order ID
        public decimal INVDATE { get; set; } // e-Invoice Date and Time
        public decimal AUDTTIME { get; set; } // e-Invoice Time
        public string INSOURCURR { get; set; } // Currency
        public decimal INRATE { get; set; } // Currency Exchange Rate
        public decimal INVNETNOTX { get; set; } // Total Excluding Tax
        public decimal INVITAXTOT { get; set; } // Total Including Tax
        public decimal INVNET { get; set; } // Total Net Amount
        public decimal INVNETWTX { get; set; } // Total Payable Amount
        public string SHPNAME { get; set; } // Shipping Reference Name
        public string SHPADDR1 { get; set; }
        public string SHPADDR2 { get; set; }
        public string SHPADDR3 { get; set; }
        public string SHPADDR4 { get; set; }
        public string REFERENCE { get; set; } // Reference Number of Custom Form 1, 9 etc
        public string TERMS { get; set; } // Incoterms
        public string CUSTOMER { get; set; } // Link to ARCUST.IDCUST
        public string CustomerBRN { get; set; }
        public string CustomerTIN { get; set; } = "EI00000000010";
        public ICollection<OrderEntryDetail> OrderEntryDetails { get; set; }
    }
}
