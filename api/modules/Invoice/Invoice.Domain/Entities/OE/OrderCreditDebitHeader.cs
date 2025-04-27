using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NexKoala.WebApi.Invoice.Domain.Entities.OE
{
    /// <summary>
    /// Represents the Order Credit/Debit Header.
    /// Maps to the "OECRDH" table.
    /// DB no data
    /// </summary>
    public class OrderCreditDebitHeader
    {
        public decimal CRDUNIQ { get; set; } // Unique Identifier for Credit/Debit Note
        public string ORDNUMBER { get; set; } // Order Number
        public string INVNUMBER { get; set; } // Invoice Number
        public string CRDNUMBER { get; set; } // Credit/Debit Note Number
        public string CUSTOMER { get; set; } // Customer Identifier
        public string BILNAME { get; set; } // Billing Name
        public string BILEMAIL { get; set; } // Billing Email
        public string BILPHONE { get; set; } // Billing Phone
        public string BILADDR1 { get; set; } // Billing Address Line 1
        public string BILADDR2 { get; set; } // Billing Address Line 2
        public string BILADDR3 { get; set; } // Billing Address Line 3
        public string BILADDR4 { get; set; } // Billing Address Line 4
        public string BILCITY { get; set; } // Buyer City
        public string BILSTATE { get; set; } // Buyer State 
        public string BILZIP { get; set; } // Buyer Zip
        public string BILCOUNTRY { get; set; } // Buyer Country
        public decimal INVDATE { get; set; } // Invoice Date
        public decimal CRDDATE { get; set; } // Credit/Debit Date
        public decimal CRDNET { get; set; } // Total Amount
        public decimal CRDNETNOTX { get; set; } // Total Excluding Tax
        public decimal CRDITAXTOT { get; set; } // Total Tax Amount
        public decimal CRDNETWTX { get; set; } // Total Payable Amount
        public string TAXGROUP { get; set; } // Tax Group Code
        public string TAUTH1 { get; set; } // Tax Authorization 1
        public string SHPNAME { get; set; } // Shipping Name
        public string SHPADDR1 { get; set; } // Shipping Address Line 1
        public string SHPADDR2 { get; set; } // Shipping Address Line 2
        public string SHPADDR3 { get; set; } // Shipping Address Line 3
        public string SHPADDR4 { get; set; } // Shipping Address Line 4
        public string CRSOURCURR { get; set; } // Invoice Currency Code
        public string CustomerBRN { get; set; }
        //public string CustomerTIN { get; set; } = "EI00000000010"; //ERR237 Buyer's details cannot be general TIN (010) for issuance of individual e-Invoice
        public string CustomerTIN { get; set; } = "C10285856100";
        //random TIN no. need verify with client where to get client TIN
        public ICollection<OrderCreditDebitDetail> OrderCreditDebitDetails { get; set; }
    }
}
