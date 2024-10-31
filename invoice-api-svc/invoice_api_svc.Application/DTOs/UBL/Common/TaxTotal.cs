namespace invoice_api_svc.Application.DTOs.UBL.Common
{
    public class TaxTotal
    {
        public Amount[] TaxAmount { get; set; }
        public TaxSubtotal[] TaxSubtotal { get; set; }
    }
}