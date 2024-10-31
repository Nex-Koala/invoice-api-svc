using invoice_api_svc.Application.DTOs.UBL.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.UBL.Invoice
{
    public class InvoiceTypeCode : BasicComponent
    {
        [JsonProperty("listVersionID")]
        public string ListVersionId { get; set; }
    }
}
