using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexKoala.Framework.Core.Exceptions;
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
    
    public async Task<bool> HasQuotaAsync(string userId, int requiredCount = 1)
    {
        var user = await _context.Partners
            .Include(u => u.LicenseKey)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user?.LicenseKey == null || user.LicenseKey.Status != LicenseStatus.Active)
            throw new GenericException("Your license is expired or your quota has used up");

        var tempSubmissionCount = user.LicenseKey.SubmissionCount + requiredCount;
        return tempSubmissionCount > user.LicenseKey.MaxSubmissions ? throw new GenericException("Insufficient quota") : true;
    }

    public async Task DeductQuotaAsync(string userId, int count = 1)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var user = await _context.Partners
                .Include(u => u.LicenseKey)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user?.LicenseKey == null)
                throw new GenericException("License not found");

            user.LicenseKey.SubmissionCount += count;
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
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
