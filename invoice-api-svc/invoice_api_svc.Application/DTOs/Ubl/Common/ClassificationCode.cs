using Newtonsoft.Json;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class ClassificationCode : BasicComponent
    {
        [JsonProperty("listID")]
        public string ListId { get; set; }
    }
}