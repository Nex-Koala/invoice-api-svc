using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Error
{
    public class ErrorDetail
    {
        public string PropertyName { get; set; }
        public string PropertyPath { get; set; }
        public string Code { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public string Target { get; set; }
        public List<ErrorDetail> Details { get; set; }
    }
}
