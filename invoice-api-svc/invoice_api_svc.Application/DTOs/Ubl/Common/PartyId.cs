using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using invoice_api_svc.Application.DTOs.Ubl.Common;
using Newtonsoft.Json;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class PartyId : BasicComponent
    {
        [JsonProperty("schemeID")]
        public string SchemeId { get; set; }
    }
}
