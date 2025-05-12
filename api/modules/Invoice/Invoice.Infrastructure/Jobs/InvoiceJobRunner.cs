using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace NexKoala.WebApi.Invoice.Infrastructure.Jobs
{
    public class InvoiceJobRunner : IHostedService
    {
        private readonly IRecurringJobManager _recurringJobs;
        private readonly ILogger<InvoiceJobRunner> _logger;

        public InvoiceJobRunner(
            IRecurringJobManager recurringJobs,
            ILogger<InvoiceJobRunner> logger)
        {
            _recurringJobs = recurringJobs ?? throw new ArgumentNullException(nameof(recurringJobs));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var timeZone = TZConvert.GetTimeZoneInfo("Asia/Kuala_Lumpur");

                _recurringJobs.AddOrUpdate<LhdnStatusJob>(
                    "lhdn-invoice-sync",
                    "invoices",
                    x => x.SyncPendingInvoices(),
                    "*/1 * * * *",
                    new RecurringJobOptions
                    {
                        TimeZone = timeZone,
                        MisfireHandling = MisfireHandlingMode.Relaxed,
                    }
                );

                _logger.LogInformation("Successfully scheduled LHDN invoice sync job");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to schedule recurring jobs");
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Cleanup if needed
            _logger.LogInformation("LHDN invoice sync job runner is stopping");
            return Task.CompletedTask;
        }
    }
}
