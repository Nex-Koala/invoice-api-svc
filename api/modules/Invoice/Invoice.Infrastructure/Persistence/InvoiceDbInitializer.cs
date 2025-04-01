using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NexKoala.WebApi.Invoice.Domain;
using NexKoala.Framework.Core.Persistence;

namespace NexKoala.WebApi.Invoice.Infrastructure.Persistence;
internal sealed class InvoiceDbInitializer(
    ILogger<InvoiceDbInitializer> logger,
    InvoiceDbContext context) : IDbInitializer
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
        {
            await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("[{Tenant}] applied database migrations for invoice module", context.TenantInfo!.Identifier);
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
    }
}
