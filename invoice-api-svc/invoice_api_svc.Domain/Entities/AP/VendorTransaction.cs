namespace invoice_api_svc.Domain.Entities.AP
{
    public class VendorTransaction
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public decimal OutstandingBalance { get; set; }
    }
}
