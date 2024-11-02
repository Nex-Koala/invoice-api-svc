using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Invoice
{
    public class Price
    {
        public Amount[] PriceAmount { get; set; }
    }
}