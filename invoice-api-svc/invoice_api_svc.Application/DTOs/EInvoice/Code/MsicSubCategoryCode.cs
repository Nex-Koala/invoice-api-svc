using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.EInvoice.Code
{
    public class MsicSubCategoryCode
    {
        public string Code { get; set; }
        public string Description { get; set; }
        [JsonProperty("MSIC Category Reference")]
        public string MsicCategoryReference { get; set; }
    }
}
