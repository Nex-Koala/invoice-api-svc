namespace invoice_api_svc.Application.DTOs.UBL.Common
{
    public class TaxSubtotal
    {
        public Amount[] TaxableAmount { get; set; }
        public Amount[] TaxAmount { get; set; }
        public TaxCategory[] TaxCategory { get; set; }
    }
}