using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.EInvoice.Document
{
    public class DocumentDetails
    {
        public string Uuid { get; set; }
        public string SubmissionUid { get; set; }
        public string LongId { get; set; }
        public string InternalId { get; set; }
        public string TypeName { get; set; }
        public string TypeVersionName { get; set; }
        public string IssuerTin { get; set; }
        public string IssuerName { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public DateTime DateTimeValidated { get; set; }
        public decimal TotalExcludingTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalNetAmount { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public string Status { get; set; }
        public DocumentValidationResult ValidationResults { get; set; }
    }
}
