namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class TaxTotal
    {
        public Amount[] TaxAmount { get; set; }
        public TaxSubtotal[] TaxSubtotal { get; set; }
    }
}
