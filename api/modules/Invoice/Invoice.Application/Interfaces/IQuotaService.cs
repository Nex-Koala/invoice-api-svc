using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Interfaces;
public interface IQuotaService
{
    Task<bool> HasQuotaAsync(string userId, int requiredCount = 1);
    Task DeductQuotaAsync(string userId, int count = 1);
    Task ResetQuota(string userId);
}
