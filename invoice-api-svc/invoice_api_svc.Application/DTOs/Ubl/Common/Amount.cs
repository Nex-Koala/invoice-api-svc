using invoice_api_svc.Application.DTOs.Ubl.Common;
using Newtonsoft.Json;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class Amount : BasicComponent<decimal>
    {
        [JsonProperty("currencyID")]
        public string CurrencyId { get; set; }
    }
}
