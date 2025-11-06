using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.RecentDocument;
using NexKoala.WebApi.Invoice.Application.Dtos.Ubl;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Domain.Settings;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Token;
using NexKoala.Framework.Core.Exceptions;
using Mapster;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRecentDocuments.v1;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Invoice;

namespace NexKoala.WebApi.Invoice.Infrastructure.Apis
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

        public async Task<SubmitInvoiceResponse> SubmitInvoiceAsync(UblDocumentRequest payload, string onBehalfOf)
        {
            var token = await GetTokenAsync(); // Get token from LHDN API

            var client = _httpClientFactory.CreateClient("LhdnApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //client.DefaultRequestHeaders.Add("onbehalfof", onBehalfOf); need register an intermediary company!

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "application/json"
            );
            var response = await client.PostAsync("/api/v1.0/documentsubmissions/", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var submissionResult = JsonConvert.DeserializeObject<SubmitInvoiceResponse>(result);
                return submissionResult;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var submissionResult = JsonConvert.DeserializeObject<RejectedDocument>(result);
                return new SubmitInvoiceResponse
                {
                    RejectedDocuments = submissionResult != null ? new List<RejectedDocument> { submissionResult } : null
                };
            }
        }

        public async Task<RawDocument> GetDocumentAsync(string uuid, string onBehalfOf)
        {
            try
            {
                var token = await GetTokenAsync();

                var client = _httpClientFactory.CreateClient("LhdnApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //client.DefaultRequestHeaders.Add("onbehalfof", onBehalfOf); need register an intermediary company!

                var response = await client.GetAsync($"/api/v1.0/documents/{uuid}/raw");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var document = JsonConvert.DeserializeObject<RawDocumentJson>(result);
                    var invoiceDocumentObj = JsonConvert.DeserializeObject<UblInvoiceDocument>(document.Document);
                    var rawDocument = document.Adapt<RawDocument>();
                    rawDocument.Document = invoiceDocumentObj;
                    return rawDocument;
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
                _logger.LogWarning(ex.Message);
                return null;
            }
        }

        public async Task<DocumentDetails> GetDocumentDetailsAsync(string uuid, string onBehalfOf)
        {
            try
            {
                var token = await GetTokenAsync(); // Get token from LHDN API

                var client = _httpClientFactory.CreateClient("LhdnApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //client.DefaultRequestHeaders.Add("onbehalfof", onBehalfOf); need register an intermediary company!

                var response = await client.GetAsync($"/api/v1.0/documents/{uuid}/details");

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

        public async Task<RecentDocuments> GetRecentDocumentsAsync(GetRecentDocuments request, string onBehalfOf)
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
                { "issuerId", request.IssuerId },
            };

                var token = await GetTokenAsync(); // Get token from LHDN API

                var client = _httpClientFactory.CreateClient("LhdnApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //client.DefaultRequestHeaders.Add("onbehalfof", onBehalfOf); need register an intermediary company!

                var queryString = QueryHelpers.AddQueryString("/api/v1.0/documents/recent", queryDictionary
                   .Where(p => !string.IsNullOrEmpty(p.Value))
                   .ToDictionary(p => p.Key, p => p.Value));

                var response = await client.GetAsync(queryString);

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
            var client = _httpClientFactory.CreateClient("LhdnApi");

            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _settings.ClientId),
                new KeyValuePair<string, string>("client_secret", _settings.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("scope", "InvoicingAPI"),
            });

            try
            {
                var response = await client.PostAsync("/connect/token", requestContent);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to generate token from LHDN. StatusCode: {StatusCode}, Response: {Response}",
                        response.StatusCode, responseContent);
                    throw new GenericException($"Failed to generate token: {response.StatusCode}");
                }

                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                if (tokenResponse == null || string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
                {
                    _logger.LogError("Token response is null or missing access token. Response content: {Response}", responseContent);
                    throw new GenericException("Token generation failed: Access token is missing.");
                }

                return tokenResponse.AccessToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while generating LHDN token.");
                throw new GenericException("An error occurred while generating token.");
            }
        }
    }
}
