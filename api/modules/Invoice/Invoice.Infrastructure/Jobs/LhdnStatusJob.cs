using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finbuckle.MultiTenant.Abstractions;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using NexKoala.Framework.Infrastructure.Jobs;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Infrastructure.Persistence;

namespace NexKoala.WebApi.Invoice.Infrastructure.Jobs;

public class LhdnStatusJob
{
    private readonly InvoiceDbContext _db;
    private readonly ILhdnApi _lhdnApi;
    private readonly IMultiTenantContextAccessor _tenantContext;

    public LhdnStatusJob(
        InvoiceDbContext db,
        ILhdnApi lhdnApi,
        IMultiTenantContextAccessor tenantContext
    )
    {
        _db = db;
        _lhdnApi = lhdnApi;
        _tenantContext = tenantContext;
    }

    [AutomaticRetry(Attempts = 3)]
    public async Task SyncPendingInvoices()
    {
        var submittedDoc = await _db
            .InvoiceDocuments.Where(d =>
                d.DocumentStatus == Domain.Entities.DocumentStatus.Submitted && d.Uuid != null
            )
            .OrderBy(d => d.LastModified)
            .Take(100)
            .ToListAsync();

        if (submittedDoc.Any())
        {
            foreach (var doc in submittedDoc)
            {
                var response = await _lhdnApi.GetDocumentDetailsAsync(doc.Uuid, "");

                if (response != null)
                {
                    if (response.Status == "Valid")
                    {
                        doc.DocumentStatus = Domain.Entities.DocumentStatus.Valid;
                        continue;
                    }
                    else if (response.Status == "Invalid")
                    {
                        doc.DocumentStatus = Domain.Entities.DocumentStatus.Invalid;
                        continue;
                    }
                    else if (response.Status == "Cancelled")
                    {
                        doc.DocumentStatus = Domain.Entities.DocumentStatus.Cancelled;
                        continue;
                    }
                    else if (response.Status == "Submitted")
                    {
                        doc.DocumentStatus = Domain.Entities.DocumentStatus.Submitted;
                        continue;
                    }
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}
