using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl
{
    public class UblInvoiceDocument
    {
        public string _D { get; set; } = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
        public string _A { get; set; } = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
        public string _B { get; set; } = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
        public List<UblInvoice> Invoice { get; set; }
    }
}
