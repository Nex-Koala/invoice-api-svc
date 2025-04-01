using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Invoice
{
    public class InvoiceLine
    {
        [JsonProperty("ID")]
        public BasicComponent[] Id { get; set; }
        public Quantity[] InvoicedQuantity { get; set; }
        public Amount[] LineExtensionAmount { get; set; }
        public Item[] Item { get; set; }
        public Price[] Price { get; set; }
        public TaxTotal[] TaxTotal { get; set; }
    }
}
