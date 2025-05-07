using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.Statistics.GetSageSubmissionRate.v1;

namespace NexKoala.WebApi.Invoice.Application.Features.Statistics.GetLhdnSubmissionRate.v1;

public record GetLhdnSubmissionRate(DateTime? StartDate, DateTime? EndDate) : IRequest<Response<List<SubmissionRateResponse>>>
{
    public string? UserId { get; init; }
};
