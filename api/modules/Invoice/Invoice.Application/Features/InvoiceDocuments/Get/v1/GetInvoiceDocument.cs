using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
public record GetInvoiceDocument(string Uuid): IRequest<Response<InvoiceDocumentResponse>>
{
}
