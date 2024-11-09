using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.EInvoice.Error
{
    public class StandardErrorResponse
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public ErrorDetail Error { get; set; }
    }
}
