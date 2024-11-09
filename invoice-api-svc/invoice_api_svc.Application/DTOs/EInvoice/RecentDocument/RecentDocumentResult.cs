using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.EInvoice.RecentDocument
{
    public class RecentDocumentResult
    {
        public string Uuid { get; set; }
        public string SubmissionUid { get; set; }
        public string LongId { get; set; }
        public string InternalId { get; set; }
        public string TypeName { get; set; }
        public string TypeVersionName { get; set; }
        public string IssuerTin { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public DateTime DateTimeValidated { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime CancelDateTime { get; set; }
        public DateTime RejectRequestDateTime { get; set; }
        public string DocumentStatusReason { get; set; }
        public string CreatedByUserId { get; set; }
        public string SupplierTin { get; set; }
        public string SupplierName { get; set; }
        public string SubmissionChannel { get; set; }
        public string IntermediaryName { get; set; }
        public string IntermediaryTin { get; set; }
        public string BuyerName { get; set; }
        public string BuyerTin { get; set; }
    }
}
