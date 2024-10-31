using Newtonsoft.Json;

namespace invoice_api_svc.Application.DTOs.UBL.Common
{
    public class TaxScheme
    {
        [JsonProperty("ID")]
        public TaxSchemeId[] Id { get; set; }
    }
}