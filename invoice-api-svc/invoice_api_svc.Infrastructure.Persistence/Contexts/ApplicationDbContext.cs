using invoice_api_svc.Application.Interfaces;
using invoice_api_svc.Domain.Common;
using invoice_api_svc.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace invoice_api_svc.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, 
            IDateTimeService dateTime, 
            IAuthenticatedUserService authenticatedUser, 
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public DbSet<Product> Products { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Extract countryCode from HttpContext
                var countryCode = _httpContextAccessor.HttpContext?.Items["CountryCode"]?.ToString();

                if (!string.IsNullOrEmpty(countryCode))
                {
                    // Create a dynamic connection string based on the countryCode
                    var connectionString = GetConnectionStringForCountryCode(countryCode);
                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        optionsBuilder.UseSqlServer(connectionString);
                    }
                    else
                    {
                        throw new Exception("Unable to determine database connection string for the country code: " + countryCode);
                    }
                }
                else
                {
                    // Fallback to default connection if no countryCode is found
                    optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
                }
            }
        }

        // Method to fetch the connection string based on country code
        private string GetConnectionStringForCountryCode(string countryCode)
        {
            // Assuming the connection strings are stored in the appsettings.json file with a prefix "ConnectionStrings:"
            return _configuration.GetConnectionString($"Connection_{countryCode}");
        }
    }
}
