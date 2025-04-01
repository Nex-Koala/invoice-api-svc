using Newtonsoft.Json;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class TaxSchemeId : BasicComponent
    {
        [JsonProperty("schemeID")]
        public string SchemeId { get; set; }
        [JsonProperty("schemeAgencyID")]
        public string SchemeAgencyId { get; set; }
    }
}
