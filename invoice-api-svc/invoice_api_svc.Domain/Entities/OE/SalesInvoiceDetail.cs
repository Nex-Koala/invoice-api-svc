namespace invoice_api_svc.Domain.Entities.OE
{
    public class SalesInvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public SalesInvoiceHeader InvoiceHeader { get; set; }
    }
}
