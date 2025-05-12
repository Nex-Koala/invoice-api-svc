using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRawDocument.v1;

public record GetRawDocument(string Uuid, string UserId, bool? IsAdmin = false) : IRequest<Response<RawDocument>>;
