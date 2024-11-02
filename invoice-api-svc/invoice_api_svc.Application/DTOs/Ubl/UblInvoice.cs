using Newtonsoft.Json;
using invoice_api_svc.Application.DTOs.Ubl.Common;
using invoice_api_svc.Application.DTOs.Ubl.Invoice;

namespace invoice_api_svc.Application.DTOs.Ubl
{
    public class UblInvoice
    {
        [JsonProperty("ID")]
        public BasicComponent[] Id { get; set; }
        public BasicComponent[] IssueDate { get; set; }
        public BasicComponent[] IssueTime { get; set; }
        public InvoiceTypeCode[] InvoiceTypeCode { get; set; }
        public BasicComponent[] DocumentCurrencyCode { get; set; }
        public InvoicePeriod[] InvoicePeriod { get; set; }
        public BillingReference[] BillingReference { get; set; }
        public DocumentReference[] AdditionalDocumentReference { get; set; }
        public AccountingSupplierParty[] AccountingSupplierParty { get; set; }
        public AccountingCustomerParty[] AccountingCustomerParty { get; set; }
        public MonetaryTotal[] LegalMonetaryTotal { get; set; }
        public Invoice.InvoiceLine[] InvoiceLine { get; set; }
        public TaxTotal[] TaxTotal { get; set; }
    }
}
