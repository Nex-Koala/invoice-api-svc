using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NexKoala.WebApi.Invoice.Infrastructure.Jobs;
public static class Extensions
{
    public static IServiceCollection AddInvoiceJobs(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IHostedService, InvoiceJobRunner>();

        services.AddScoped<LhdnStatusJob>();

        return services;
    }
}
