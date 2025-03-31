using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code
{
    public class PaymentMethodResponse
    {
        public string Code { get; set; }
        [JsonProperty("Payment Method")]
        public string PaymentMethod { get; set; }
    }
}
