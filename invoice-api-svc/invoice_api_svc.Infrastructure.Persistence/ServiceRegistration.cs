using invoice_api_svc.Application.Interfaces;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Infrastructure.Persistence.Contexts;
using invoice_api_svc.Infrastructure.Persistence.Repositories;
using invoice_api_svc.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace invoice_api_svc.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddDbContext<ClientDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ClientConnection"),
                    b => b.MigrationsAssembly(typeof(ClientDbContext).Assembly.FullName)));

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddTransient<IInvoiceDocumentRepositoryAsync, InvoiceDocumentRepositoryAsync>();
            services.AddTransient<IUomRepositoryAsync, UomRepositoryAsync>();
            services.AddTransient<IUomMappingRepositoryAsync, UomMappingRepositoryAsync>();
            services.AddTransient<IUserRepositoryAsync, UserRepositoryAsync>();
            #endregion
        }
    }
}
