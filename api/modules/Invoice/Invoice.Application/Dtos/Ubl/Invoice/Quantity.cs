using Newtonsoft.Json;
using NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Invoice
{
    public class Quantity : BasicComponent<decimal>
    {
        [JsonProperty("unitCode")]
        public string UnitCode { get; set; }
    }
}
