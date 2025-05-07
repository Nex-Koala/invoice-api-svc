using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexKoala.Framework.Core.Exceptions;

namespace NexKoala.WebApi.Invoice.Domain.Exceptions;
public class InvoiceDocumentNotFoundException : NotFoundException
{
    public InvoiceDocumentNotFoundException(Guid id)
        : base($"InvoiceDocument with id {id} not found")
    {
    }

    public InvoiceDocumentNotFoundException(string uuid)
        : base($"InvoiceDocument with uuid {uuid} not found")
    {
    }
}
