using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Infrastructure.Persistence;

namespace NexKoala.WebApi.Invoice.Infrastructure.Services;
public class QuotaService: IQuotaService
{
    private readonly InvoiceDbContext _context;
    public QuotaService(InvoiceDbContext context)
    {
        _context = context;
    }
    public async Task<bool> TryAcquireQuota(string userId)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var user = await _context.Partners
                .Include(u => u.LicenseKey)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null || user.LicenseKey == null)
            {
                return false;
            }

            var license = user.LicenseKey;

            if (license.Status != LicenseStatus.Active)
            {
                return false;
            }

            license.SubmissionCount++;
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task ResetQuota(string userId)
    {
        var user = await _context.Partners.Where(u => u.UserId == userId)
                .FirstOrDefaultAsync();

        if (user != null)
        {
            user.LicenseKey.SubmissionCount = 0;
            await _context.SaveChangesAsync();
        }
    }
}
