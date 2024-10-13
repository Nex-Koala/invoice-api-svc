using invoice_api_svc.Domain.Entities;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Interfaces.Repositories
{
    public interface IInvoiceDocumentRepositoryAsync : IGenericRepositoryAsync<InvoiceDocument>
    {
    }
}
