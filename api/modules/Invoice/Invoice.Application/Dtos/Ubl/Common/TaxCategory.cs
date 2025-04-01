using Newtonsoft.Json;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class TaxCategory
    {
        [JsonProperty("ID")]
        public BasicComponent[] Id { get; set; }
        public TaxScheme[] TaxScheme { get; set; }
    }
}
