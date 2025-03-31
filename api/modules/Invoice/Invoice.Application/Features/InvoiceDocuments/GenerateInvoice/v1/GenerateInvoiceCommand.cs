using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GenerateInvoice.v1;

public record GenerateInvoiceCommand(string Uuid) : IRequest<Response<byte[]>>;
