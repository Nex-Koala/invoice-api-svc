using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetClassificationCodes.v1;

public record GetClassificationCodes : IRequest<Response<List<ClassificationCode>>>;
