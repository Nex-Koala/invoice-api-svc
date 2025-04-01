namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
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
