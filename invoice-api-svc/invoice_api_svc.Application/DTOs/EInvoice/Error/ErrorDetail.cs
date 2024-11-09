using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.EInvoice.Error
{
    public class ErrorDetail
    {
        public string PropertyName { get; set; }
        public string PropertyPath { get; set; }
        public string ErrorCode { get; set; }
        public string Error { get; set; }
        public string ErrorMS { get; set; }
        public string Target { get; set; }
        public List<ErrorDetail> InnerError { get; set; }
    }
}
