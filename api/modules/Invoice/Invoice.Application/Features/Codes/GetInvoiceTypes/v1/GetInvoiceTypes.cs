using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetInvoiceTypes.v1;

public record GetInvoiceTypes : IRequest<Response<List<EInvoiceType>>>;
