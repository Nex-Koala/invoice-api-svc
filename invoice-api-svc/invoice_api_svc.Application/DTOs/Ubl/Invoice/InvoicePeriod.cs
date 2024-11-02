using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Invoice
{
    public class InvoicePeriod
    {
        public BasicComponent[] StartDate { get; set; }
        public BasicComponent[] EndDate { get; set; }
        public BasicComponent[] Description { get; set; }
    }
}
