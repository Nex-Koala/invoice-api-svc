using System;
using System.Collections.Generic;
using System.Text;

namespace invoice_api_svc.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
