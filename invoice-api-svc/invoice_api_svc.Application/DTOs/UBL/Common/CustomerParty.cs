using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.UBL.Common
{
    public class CustomerParty
    {
        public PartyIdentification[] PartyIdentification { get; set; }
        public PostalAddress[] PostalAddress { get; set; }
        public PartyLegalEntity[] PartyLegalEntity { get; set; }
        public Contact[] Contact { get; set; }
    }
}
