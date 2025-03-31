using NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Invoice
{
    public class Item
    {
        public BasicComponent[] Description { get; set; }
        public CommodityClassification[] CommodityClassification { get; set; }
        public Country[] OriginCountry { get; set; }
    }
}