using NexKoala.Framework.Infrastructure;
using NexKoala.Framework.Infrastructure.Logging.Serilog;
using NexKoala.WebApi.Host;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using Serilog;

StaticLogger.EnsureInitialized();
Log.Information("server booting up..");
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureNexKoalaFramework();
    builder.RegisterModules();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var msicService = scope.ServiceProvider.GetRequiredService<IMsicService>();
        await msicService.InitializeAsync();
    }

    app.UseNexKoalaFramework();
    app.UseModules();
    await app.RunAsync();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("HostAbortedException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("server shutting down..");
    await Log.CloseAndFlushAsync();
}
