using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetCurrencyCodes.v1;

public record GetCurrencyCodes : IRequest<Response<List<CurrencyCode>>>;
