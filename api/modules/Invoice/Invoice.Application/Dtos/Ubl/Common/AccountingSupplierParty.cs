using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class AccountingSupplierParty
    {
        [JsonProperty("AdditionalAccountID")]
        public AdditionalAccountId[] AdditionalAccountId { get; set; }
        public SupplierParty[] Party { get; set; }
    }
}
