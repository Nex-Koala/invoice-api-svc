using invoice_api_svc.Application.Interfaces;
using invoice_api_svc.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Infrastructure.Persistence.Repository
{
    public class MultiTenancyMiddleware
    {
        private readonly RequestDelegate _next;

        public MultiTenancyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Extract 'countryCode' from the route (Assumes /api/{countryCode}/v1/xxx)
            var pathSegments = context.Request.Path.Value.Split('/');
            if (pathSegments.Length > 2)
            {
                var countryCode = pathSegments[2]; // Gets the {countryCode} part from the path

                if (!string.IsNullOrEmpty(countryCode))
                {
                    // Store the countryCode in HttpContext.Items
                    context.Items["CountryCode"] = countryCode;
                }
            }

            // Continue the request pipeline
            await _next(context);
        }
    }
}
