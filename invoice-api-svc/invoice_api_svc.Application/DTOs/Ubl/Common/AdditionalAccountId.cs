using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class AdditionalAccountId : BasicComponent
    {
        [JsonProperty("schemeAgencyName")]
        public string SchemeAgencyName { get; set; }
    }
}
