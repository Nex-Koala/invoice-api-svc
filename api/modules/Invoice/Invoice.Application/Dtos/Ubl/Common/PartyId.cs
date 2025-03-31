using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class PartyId : BasicComponent
    {
        [JsonProperty("schemeID")]
        public string SchemeId { get; set; }
    }
}
