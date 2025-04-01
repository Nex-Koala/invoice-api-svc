using Newtonsoft.Json;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class TaxScheme
    {
        [JsonProperty("ID")]
        public TaxSchemeId[] Id { get; set; }
    }
}
