using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class TaxTotal
    {
        public Amount[] TaxAmount { get; set; }
        public TaxSubtotal[] TaxSubtotal { get; set; }
    }
}
