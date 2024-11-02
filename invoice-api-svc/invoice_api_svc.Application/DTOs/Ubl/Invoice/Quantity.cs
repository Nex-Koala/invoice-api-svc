using invoice_api_svc.Application.DTOs.Ubl.Common;
using Newtonsoft.Json;

namespace invoice_api_svc.Application.DTOs.Ubl.Invoice
{
    public class Quantity : BasicComponent<decimal>
    {
        [JsonProperty("unitCode")]
        public string UnitCode { get; set; }
    }
}
