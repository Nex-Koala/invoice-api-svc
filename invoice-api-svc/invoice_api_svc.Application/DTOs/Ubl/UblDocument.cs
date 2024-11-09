using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.Ubl
{
    public class UblDocument
    {
        [JsonProperty("format")]
        public string Format { get; set; } = "JSON";
        [JsonProperty("documentHash")]
        public string DocumentHash { get; set; }
        [JsonProperty("codeNumber")]
        public string CodeNumber { get; set; }
        [JsonProperty("document")]
        public string Document { get; set; }
    }
}
