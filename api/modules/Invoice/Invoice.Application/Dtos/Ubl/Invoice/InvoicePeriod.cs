using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Invoice
{
    public class InvoicePeriod
    {
        public BasicComponent[] StartDate { get; set; }
        public BasicComponent[] EndDate { get; set; }
        public BasicComponent[] Description { get; set; }
    }
}
