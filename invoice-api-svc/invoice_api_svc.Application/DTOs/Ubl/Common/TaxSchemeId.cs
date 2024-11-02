using Newtonsoft.Json;
using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class TaxSchemeId : BasicComponent
    {
        [JsonProperty("schemeID")]
        public string SchemeId { get; set; }
        [JsonProperty("schemeAgencyID")]
        public string SchemeAgencyId { get; set; }
    }
}
