using System.Collections.Generic;

namespace invoice_api_svc.Application.Interfaces
{
    public interface ITrimStringService
    {
        T TrimStringProperties<T>(T obj);
    }
}
