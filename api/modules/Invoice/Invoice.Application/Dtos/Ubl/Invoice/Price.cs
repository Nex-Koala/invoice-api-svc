using NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Invoice
{
    public class Price
    {
        public Amount[] PriceAmount { get; set; }
    }
}