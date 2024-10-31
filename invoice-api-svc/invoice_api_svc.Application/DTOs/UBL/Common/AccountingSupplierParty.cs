using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.UBL.Common
{
    public class AccountingSupplierParty
    {
        [JsonProperty("AdditionalAccountID")]
        public AdditionalAccountId[] AdditionalAccountId { get; set; }
        public SupplierParty[] Party { get; set; }
    }
}
