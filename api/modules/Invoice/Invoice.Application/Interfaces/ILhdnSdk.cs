using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Interfaces
{
    public interface ILhdnSdk
    {
        Task<List<ClassificationCode>> GetClassificationCodesAsync();
        Task<List<CurrencyCode>> GetCurrencyCodesAsync();
        Task<List<EInvoiceType>> GetInvoiceTypesAsync();
        Task<List<MsicSubCategoryCode>> GetMsicCodesAsync();
        Task<List<PaymentMethodResponse>> GetPaymentMethodsAsync();
        Task<List<StateCode>> GetStateCodesAsync();
        Task<List<TaxType>> GetTaxTypesAsync();
        Task<List<UnitType>> GetUnitTypesAsync();
    }
}
