using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Interfaces;
public interface IQuotaService
{
    Task<bool> TryAcquireQuota(string userId);
    Task ResetQuota(string userId);
}
