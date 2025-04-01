namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class TaxSubtotal
    {
        public Amount[] TaxableAmount { get; set; }
        public Amount[] TaxAmount { get; set; }
        public TaxCategory[] TaxCategory { get; set; }
    }
}
