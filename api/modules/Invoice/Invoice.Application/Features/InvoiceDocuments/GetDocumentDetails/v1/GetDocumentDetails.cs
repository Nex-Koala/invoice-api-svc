using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetDocumentDetails.v1;

public record GetDocumentDetails(string Uuid, string OnBehalfOf) : IRequest<Response<DocumentDetails>>;
