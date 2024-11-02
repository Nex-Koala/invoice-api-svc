using Newtonsoft.Json;
using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class TaxScheme
    {
        [JsonProperty("ID")]
        public TaxSchemeId[] Id { get; set; }
    }
}
