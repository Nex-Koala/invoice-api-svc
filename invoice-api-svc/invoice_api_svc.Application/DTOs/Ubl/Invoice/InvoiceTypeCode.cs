using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Invoice
{
    public class InvoiceTypeCode : BasicComponent
    {
        [JsonProperty("listVersionID")]
        public string ListVersionId { get; set; }
    }
}
