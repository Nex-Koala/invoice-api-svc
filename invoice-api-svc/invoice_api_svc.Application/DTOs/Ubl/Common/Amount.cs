using Newtonsoft.Json;
using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class Amount : BasicComponent
    {
        [JsonProperty("currencyID")]
        public string CurrencyId { get; set; }
    }
}
