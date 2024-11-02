using Newtonsoft.Json;
using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class TaxCategory
    {
        [JsonProperty("ID")]
        public BasicComponent[] Id { get; set; }
        public TaxScheme[] TaxScheme { get; set; }
    }
}
