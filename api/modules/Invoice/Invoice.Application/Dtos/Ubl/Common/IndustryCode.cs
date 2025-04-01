using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class IndustryCode : BasicComponent
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

}
