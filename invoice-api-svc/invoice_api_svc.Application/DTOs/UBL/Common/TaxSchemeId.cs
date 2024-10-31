using Newtonsoft.Json;

namespace invoice_api_svc.Application.DTOs.UBL.Common
{
    public class TaxSchemeId : BasicComponent
    {
        [JsonProperty("schemeID")]
        public string SchemeId { get; set; }
        [JsonProperty("schemeAgencyID")]
        public string SchemeAgencyId { get; set; }
    }
}