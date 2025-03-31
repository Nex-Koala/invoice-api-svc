using Newtonsoft.Json;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class Amount : BasicComponent<decimal>
    {
        [JsonProperty("currencyID")]
        public string CurrencyId { get; set; }
    }
}
