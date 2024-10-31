namespace invoice_api_svc.Application.DTOs.UBL.Common
{
    public class MonetaryTotal
    {
        public Amount[] PayableAmount { get; set; }
        public Amount[] TaxExclusiveAmount { get; set; }
        public Amount[] TaxInclusiveAmount { get; set; }
    }
}