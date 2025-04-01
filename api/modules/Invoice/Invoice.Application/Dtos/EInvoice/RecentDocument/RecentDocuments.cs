using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.RecentDocument
{
    public class RecentDocuments
    {
        public List<RecentDocumentResult> Result { get; set; }
        public RecentDocumentMetadata Metadata { get; set; }
    }
}
