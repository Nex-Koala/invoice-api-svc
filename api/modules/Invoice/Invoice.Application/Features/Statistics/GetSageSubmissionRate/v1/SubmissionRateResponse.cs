using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Features.Statistics.GetSageSubmissionRate.v1;
public class SubmissionRateResponse
{
    public string Label { get; set; } = string.Empty;
    public int Value { get; set; }
}
