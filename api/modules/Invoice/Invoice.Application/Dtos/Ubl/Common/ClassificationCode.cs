using Newtonsoft.Json;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class ClassificationCode : BasicComponent
    {
        [JsonProperty("listID")]
        public string ListId { get; set; }
    }
}