using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Domain.Entities;
using invoice_api_svc.Infrastructure.Persistence.Contexts;
using invoice_api_svc.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace invoice_api_svc.Infrastructure.Persistence.Repositories
{
    public class InvoiceDocumentRepositoryAsync : GenericRepositoryAsync<InvoiceDocument>, IInvoiceDocumentRepositoryAsync
    {
        private readonly DbSet<InvoiceDocument> invoices;

        public InvoiceDocumentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            invoices = dbContext.Set<InvoiceDocument>();
        }
    }
}
