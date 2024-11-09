using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using invoice_api_svc.Application.DTOs.EInvoice.Document;
using invoice_api_svc.Application.DTOs.EInvoice.RecentDocument;
using invoice_api_svc.Application.DTOs.EInvoice.Token;
using invoice_api_svc.Application.DTOs.Ubl;
using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Features.InvoiceDocuments.Queries.GetRecentDocuments;
using invoice_api_svc.Application.Interfaces.Apis;
using invoice_api_svc.Domain.Settings;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace invoice_api_svc.Infrastructure.Shared.Apis
{
    public class LhdnApi : ILhdnApi
    {
        private readonly ILogger<LhdnApi> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly EInvoiceSettings _settings;

        public LhdnApi(ILogger<LhdnApi> logger, IHttpClientFactory httpClientFactory, IOptions<EInvoiceSettings> options)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _settings = options.Value;
        }

        public async Task<HttpResponseMessage> SubmitInvoiceAsync(UblDocumentRequest payload)
        {
            var token = await GetTokenAsync(); // Get token from LHDN API

            var client = _httpClientFactory.CreateClient("Lhdn");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "application/json"
            );
            var response = await client.PostAsync("/api/v1.0/documentsubmissions/", requestContent);
            return response;
        }

        public async Task<DocumentDetails> GetDocumentDetailsAsync(string uuid)
        {
            try
            {
                var token = await GetTokenAsync(); // Get token from LHDN API

                var client = _httpClientFactory.CreateClient("Lhdn");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"/documents/{uuid}/details");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DocumentDetails>(result); // Return the result as JSON
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning(result);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RecentDocuments> GetRecentDocumentsAsync(GetRecentDocumentsQuery request)
        {
            try
            {
                var queryDictionary = new Dictionary<string, string?>()
            {
                { "pageNo", request.PageNo.ToString() },
                { "pageSize", request.PageSize.ToString() },
                { "submissionDateFrom", request.SubmissionDateFrom },
                { "submissionDateTo", request.SubmissionDateTo },
                { "issueDateFrom", request.IssueDateFrom },
                { "issueDateTo", request.IssueDateTo },
                { "direction", request.Direction },
                { "status", request.Status },
                { "documentType", request.DocumentType },
                { "receiverIdType", request.ReceiverIdType },
                { "receiverId", request.ReceiverId },
                { "receiverTin", request.ReceiverTin },
                { "issuerTin", request.IssuerTin },
                { "issuerIdType", request.IssuerIdType },
                { "issuerId", request.IssuerId }
            };

                var token = await GetTokenAsync(); // Get token from LHDN API

                var client = _httpClientFactory.CreateClient("Lhdn");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var url = new UriBuilder($"/documents/recent");
                url.Query = QueryHelpers.AddQueryString(string.Empty, queryDictionary
                    .Where(p => !string.IsNullOrEmpty(p.Value))
                    .ToDictionary(p => p.Key, p => p.Value));

                var response = await client.GetAsync(url.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RecentDocuments>(result); // Return the result as JSON
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<string> GetTokenAsync()
        {
            var token = string.Empty;

            var client = _httpClientFactory.CreateClient("Lhdn");
            var requestContent = new StringContent(
                new FormUrlEncodedContent(
                    new[]
                    {
                            new KeyValuePair<string, string>("client_id", _settings.ClientId), // Replace with your actual client_id
                            new KeyValuePair<string, string>(
                                "client_secret",
                                _settings.ClientSecret
                            ), // Replace with your actual client_secret
                            new KeyValuePair<string, string>("grant_type", "client_credentials"),
                            new KeyValuePair<string, string>("scope", "InvoicingAPI"),
                    }
                )
                    .ReadAsStringAsync()
                    .Result,
                Encoding.UTF8,
                "application/x-www-form-urlencoded"
            );

            var response = await client.PostAsync("/connect/token", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(
                    responseContent
                );
                token = tokenResponse.AccessToken;
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ApiException("Failed to generate token");
            }

            return token;
        }
    }
}
