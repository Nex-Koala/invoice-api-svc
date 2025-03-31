using Newtonsoft.Json;
using NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common;
using NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Invoice;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl
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
        public InvoiceLine[] InvoiceLine { get; set; }
        public TaxTotal[] TaxTotal { get; set; }
    }
}
