using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetMsicCodes.v1;

public record GetMsicCodes : IRequest<Response<List<MsicSubCategoryCode>>>;
