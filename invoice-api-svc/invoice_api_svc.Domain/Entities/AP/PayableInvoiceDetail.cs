namespace invoice_api_svc.Domain.Entities.AP
{
    public class PayableInvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public string ItemDescription { get; set; }
        public decimal Amount { get; set; }
        public PayableInvoiceHeader InvoiceHeader { get; set; }
    }
}
