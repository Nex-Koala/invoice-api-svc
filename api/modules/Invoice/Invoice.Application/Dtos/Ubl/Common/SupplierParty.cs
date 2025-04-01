using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class SupplierParty
    {
        public IndustryCode[] IndustryClassificationCode { get; set; }
        public PartyIdentification[] PartyIdentification { get; set; }
        public PostalAddress[] PostalAddress { get; set; }
        public PartyLegalEntity[] PartyLegalEntity { get; set; }
        public Contact[] Contact { get; set; }
    }
}
