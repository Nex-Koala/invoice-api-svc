using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class IdentificationCode : BasicComponent
    {
        [JsonProperty("listID")]
        public string ListId { get; set; }
        [JsonProperty("listAgencyID")]
        public string ListAgencyId { get; set; }
    }
}
