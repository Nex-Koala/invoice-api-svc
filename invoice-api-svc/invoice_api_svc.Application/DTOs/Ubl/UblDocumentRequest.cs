using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.Ubl
{
    public class UblDocumentRequest
    {
        [JsonProperty("documents")]
        public UblDocument[] Documents { get; set; }
    }
}
