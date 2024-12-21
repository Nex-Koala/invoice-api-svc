namespace invoice_api_svc.Domain.Entities.AR
{
    public class ReceivableInvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public string ItemDescription { get; set; }
        public decimal Amount { get; set; }
        public ReceivableInvoiceHeader InvoiceHeader { get; set; }
    }
}
