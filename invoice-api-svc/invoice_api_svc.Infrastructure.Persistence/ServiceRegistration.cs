using invoice_api_svc.Application.Interfaces;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Infrastructure.Persistence.Contexts;
using invoice_api_svc.Infrastructure.Persistence.Repositories;
using invoice_api_svc.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace invoice_api_svc.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("invoice_api_svc.Infrastructure.Persistence")));
            }
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddTransient<IInvoiceDocumentRepositoryAsync, InvoiceDocumentRepositoryAsync>();
            #endregion
        }
    }
}
