using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.EInvoice.RecentDocument
{
    public class RecentDocuments
    {
        public List<RecentDocumentResult> Result { get; set; }
        public RecentDocumentMetadata Metadata { get; set; }
    }
}
