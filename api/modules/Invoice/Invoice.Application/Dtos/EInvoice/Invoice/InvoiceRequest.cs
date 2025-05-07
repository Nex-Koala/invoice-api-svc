using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Invoice
{
    public class InvoiceRequest
    {
        public string Irn { get; set; }
        public string IssueDate { get; set; }
        public string IssueTime { get; set; }
        public string InvoiceTypeCode { get; set; }
        public string CurrencyCode { get; set; }

        // New Fields
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string InvoicePeriodDescription { get; set; }
        public string BillingReferenceID { get; set; }
        public string AdditionalDocumentReferenceID { get; set; }

        // Supplier (AccountingSupplierParty) Fields
        public string SupplierAdditionalAccountID { get; set; }
        public string SupplierIndustryCode { get; set; }
        public string SupplierTIN { get; set; }
        public string SupplierIdType { get; set; }
        public string SupplierBRN { get; set; }
        public string SupplierSST { get; set; }
        public string SupplierTTX { get; set; }
        public string SupplierBusinessActivityDescription { get; set; }
        public string SupplierCity { get; set; }
        public string SupplierPostalCode { get; set; }
        public string SupplierCountrySubentityCode { get; set; }
        public string SupplierAddressLine1 { get; set; }
        public string SupplierAddressLine2 { get; set; }
        public string SupplierAddressLine3 { get; set; }
        public string SupplierCountryCode { get; set; }
        public string SupplierName { get; set; }
        public string SupplierTelephone { get; set; }
        public string SupplierEmail { get; set; }

        // Customer (AccountingCustomerParty) Fields
        public string CustomerTIN { get; set; }
        public string CustomerBRN { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerPostalCode { get; set; }
        public string CustomerCountrySubentityCode { get; set; }
        public string CustomerAddressLine1 { get; set; }
        public string CustomerAddressLine2 { get; set; }
        public string CustomerAddressLine3 { get; set; }
        public string CustomerCountryCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTelephone { get; set; }
        public string CustomerEmail { get; set; }

        // Invoice Line Items
        public decimal TotalPayableAmount { get; set; }
        public List<InvoiceItemRequest> ItemList { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalIncludingTax { get; set; }
        public decimal TotalExcludingTax { get; set; }
    }
}
