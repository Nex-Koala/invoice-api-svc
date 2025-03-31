namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class MonetaryTotal
    {
        public Amount[] PayableAmount { get; set; }
        public Amount[] TaxExclusiveAmount { get; set; }
        public Amount[] TaxInclusiveAmount { get; set; }
    }
}
