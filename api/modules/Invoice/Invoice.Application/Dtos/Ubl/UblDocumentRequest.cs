using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl
{
    public class UblDocumentRequest
    {
        [JsonProperty("documents")]
        public UblDocument[] Documents { get; set; }
    }
}
