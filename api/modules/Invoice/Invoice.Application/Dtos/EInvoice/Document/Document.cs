using NexKoala.WebApi.Invoice.Application.Dtos.Ubl;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document
{
    public class DocumentResponse
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
        public DateTime DateTimeIssued { get; set; }
        public decimal TotalExcludingTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalNetAmount { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public string Status { get; set; }
        public string DocumentStatusReason { get; set; }
        public Date CancelDateTime { get; set; }
        public Date RejectRequestDateTime { get; set; }
        public string CreatedByUserId { get; set; }

    }

    public class RawDocumentJson : DocumentResponse
    {
        public string Document { get; set; }
    }

    public class RawDocument : DocumentResponse
    {
        public UblInvoiceDocument Document { get; set; }
    }
}
