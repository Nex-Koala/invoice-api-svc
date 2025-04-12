using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Error;

namespace NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Invoice;
public class SubmitInvoiceResponse
{
    public string SubmissionUid { get; set; }
    public List<AcceptedDocument> AcceptedDocuments { get; set; }
    public List<RejectedDocument> RejectedDocuments { get; set; }
}

public class AcceptedDocument
{
    public string Uuid { get; set; }
    public string InvoiceCodeNumber { get; set; }
}

public class RejectedDocument
{
    public string InvoiceCodeNumber { get; set; }
    public ErrorDetail Error { get; set; }
}
