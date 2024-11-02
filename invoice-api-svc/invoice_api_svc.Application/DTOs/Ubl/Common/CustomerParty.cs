using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class CustomerParty
    {
        public PartyIdentification[] PartyIdentification { get; set; }
        public PostalAddress[] PostalAddress { get; set; }
        public PartyLegalEntity[] PartyLegalEntity { get; set; }
        public Contact[] Contact { get; set; }
    }
}
