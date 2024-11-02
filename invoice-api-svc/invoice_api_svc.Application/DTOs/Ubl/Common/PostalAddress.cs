using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class PostalAddress
    {
        public BasicComponent[] CityName { get; set; }
        public BasicComponent[] PostalZone { get; set; }
        public BasicComponent[] CountrySubentityCode { get; set; }
        public AddressLine[] AddressLine { get; set; }
        public Country[] Country { get; set; }
    }
}
