using invoice_api_svc.Application.DTOs.UBL.Common;
using invoice_api_svc.Application.DTOs.UBL.Invoice;
using invoice_api_svc.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.UBL
{
    public class UBLInvoice
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
