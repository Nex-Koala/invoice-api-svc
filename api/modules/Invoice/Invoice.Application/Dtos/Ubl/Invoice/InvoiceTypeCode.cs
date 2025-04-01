using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Invoice
{
    public class InvoiceTypeCode : BasicComponent
    {
        [JsonProperty("listVersionID")]
        public string ListVersionId { get; set; }
    }
}
