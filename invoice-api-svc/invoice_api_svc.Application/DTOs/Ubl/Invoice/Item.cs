using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Invoice
{
    public class Item
    {
        public BasicComponent[] Description { get; set; }
        public CommodityClassification[] CommodityClassification { get; set; }
        public Country[] OriginCountry { get; set; }
    }
}