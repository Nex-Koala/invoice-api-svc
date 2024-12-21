namespace invoice_api_svc.Domain.Entities.OE
{
    public class OrderCreditDebitDetail
    {
        public int CreditDebitDetailId { get; set; }
        public int CreditDebitId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public OrderCreditDebitHeader CreditDebitHeader { get; set; }
    }
}
