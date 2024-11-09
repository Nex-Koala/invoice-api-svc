using invoice_api_svc.Application;
using invoice_api_svc.Application.Interfaces;
using invoice_api_svc.Infrastructure.Identity;
using invoice_api_svc.Infrastructure.Persistence;
using invoice_api_svc.Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using invoice_api_svc.WebApi.Extensions;
using invoice_api_svc.WebApi.Services;
using invoice_api_svc.Infrastructure.Persistence.Repository;
using System;
using System.Configuration;
using System.Runtime;
using invoice_api_svc.Domain.Settings;

namespace invoice_api_svc.WebApi
{
    public class Startup
    {
        public IConfiguration _config { get; }
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(_config);
            services.AddPersistenceInfrastructure(_config);
            services.AddSharedInfrastructure(_config);
            services.AddSwaggerExtension();
            services.AddControllers();
            services.AddApiVersioningExtension();
            services.AddHealthChecks();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            services.AddHttpClient("Lhdn", client =>
            {
                client.BaseAddress = new Uri(_config.GetSection("EInvoiceSettings:ApiBaseUrl").Value);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            services.Configure<EInvoiceSettings>(_config.GetSection("EInvoiceSettings"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseMiddleware<MultiTenancyMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerExtension();
            app.UseErrorHandlingMiddleware();
            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
             });
        }
    }
}
