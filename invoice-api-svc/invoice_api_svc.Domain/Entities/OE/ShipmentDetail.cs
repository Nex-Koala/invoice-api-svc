namespace invoice_api_svc.Domain.Entities.OE
{
    public class ShipmentDetail
    {
        public int ShipmentDetailId { get; set; }
        public int ShipmentId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public ShipmentHeader ShipmentHeader { get; set; }
    }
}
