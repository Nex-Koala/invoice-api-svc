using Newtonsoft.Json;

namespace invoice_api_svc.Application.DTOs.UBL.Common
{
    public class TaxCategory
    {
        [JsonProperty("ID")]
        public BasicComponent[] Id { get; set; }
        public TaxScheme[] TaxScheme { get; set; }
    }
}