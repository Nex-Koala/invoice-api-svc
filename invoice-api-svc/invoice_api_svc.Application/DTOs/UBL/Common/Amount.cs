using Newtonsoft.Json;

namespace invoice_api_svc.Application.DTOs.UBL.Common
{
    public class Amount : BasicComponent
    {
        [JsonProperty("currencyID")]
        public string CurrencyId { get; set; }
    }
}