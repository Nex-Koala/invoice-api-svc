using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Statistics.GetSageSubmissionRate.v1;

public record GetSageSubmissionRate(DateTime? StartDate, DateTime? EndDate) : IRequest<Response<List<SubmissionRateResponse>>>;
