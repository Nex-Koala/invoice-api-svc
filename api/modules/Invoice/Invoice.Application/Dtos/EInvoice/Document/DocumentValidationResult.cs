using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document
{
    public class DocumentValidationResult
    {
        public string Status { get; set; }
        public List<DocumentValidationStep> ValidationSteps { get; set; }
    }
}
