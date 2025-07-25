using System;
using NodaMoney;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GenerateInvoice.FromDb
{
    public static class CurrencyHelper
    {
        /// <summary>
        /// Returns the currency symbol for a given ISO 4217 currency code.
        /// </summary>
        /// <param name="currencyCode">The currency code (e.g., "USD", "MYR", "SGD").</param>
        /// <returns>The corresponding symbol (e.g., "$", "RM", "S$"). Falls back to code if unknown.</returns>
        public static string GetCurrencySymbol(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                return string.Empty;

            try
            {
                var currency = Currency.FromCode(currencyCode.ToUpper());
                return currency.Symbol;
            }
            catch
            {
                return currencyCode.ToUpper(); // fallback if code is invalid or unsupported
            }
        }
    }
}
