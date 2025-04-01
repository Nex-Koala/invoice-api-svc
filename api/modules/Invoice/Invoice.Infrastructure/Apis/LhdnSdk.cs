using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Infrastructure.Apis
{
    public class LhdnSdk : ILhdnSdk
    {
        private readonly ILogger<LhdnSdk> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public LhdnSdk(ILogger<LhdnSdk> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<UnitType>> GetUnitTypesAsync()
        {
            var client = _httpClientFactory.CreateClient("LhdnSdk");
            var url = "/files/UnitTypes.json";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var codes = JsonConvert.DeserializeObject<List<UnitType>>(json);

                    return codes;
                }
                else
                {
                    _logger.LogWarning(await response.Content.ReadAsStringAsync());
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<ClassificationCode>> GetClassificationCodesAsync()
        {
            var client = _httpClientFactory.CreateClient("LhdnSdk");
            var url = "/files/ClassificationCodes.json";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var codes = JsonConvert.DeserializeObject<List<ClassificationCode>>(json);

                    return codes;
                }
                else
                {
                    _logger.LogWarning(await response.Content.ReadAsStringAsync());
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<MsicSubCategoryCode>> GetMsicCodesAsync()
        {
            var client = _httpClientFactory.CreateClient("LhdnSdk");
            var url = "/files/MSICSubCategoryCodes.json";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var codes = JsonConvert.DeserializeObject<List<MsicSubCategoryCode>>(json);

                    return codes;
                }
                else
                {
                    _logger.LogWarning(await response.Content.ReadAsStringAsync());
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<EInvoiceType>> GetInvoiceTypesAsync()
        {
            var client = _httpClientFactory.CreateClient("LhdnSdk");
            var url = "/files/EInvoiceTypes.json";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var codes = JsonConvert.DeserializeObject<List<EInvoiceType>>(json);

                    return codes;
                }
                else
                {
                    _logger.LogWarning(await response.Content.ReadAsStringAsync());
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<CurrencyCode>> GetCurrencyCodesAsync()
        {
            var client = _httpClientFactory.CreateClient("LhdnSdk");
            var url = "/files/CurrencyCodes.json";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var codes = JsonConvert.DeserializeObject<List<CurrencyCode>>(json);

                    return codes;
                }
                else
                {
                    _logger.LogWarning(await response.Content.ReadAsStringAsync());
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<PaymentMethodResponse>> GetPaymentMethodsAsync()
        {
            var client = _httpClientFactory.CreateClient("LhdnSdk");
            var url = "/files/PaymentMethods.json";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var codes = JsonConvert.DeserializeObject<List<PaymentMethodResponse>>(json);

                    return codes;
                }
                else
                {
                    _logger.LogWarning(await response.Content.ReadAsStringAsync());
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<StateCode>> GetStateCodesAsync()
        {
            var client = _httpClientFactory.CreateClient("LhdnSdk");
            var url = "/files/StateCodes.json";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var codes = JsonConvert.DeserializeObject<List<StateCode>>(json);

                    return codes;
                }
                else
                {
                    _logger.LogWarning(await response.Content.ReadAsStringAsync());
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<TaxType>> GetTaxTypesAsync()
        {
            var client = _httpClientFactory.CreateClient("LhdnSdk");
            var url = "/files/TaxTypes.json";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var codes = JsonConvert.DeserializeObject<List<TaxType>>(json);

                    return codes;
                }
                else
                {
                    _logger.LogWarning(await response.Content.ReadAsStringAsync());
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
