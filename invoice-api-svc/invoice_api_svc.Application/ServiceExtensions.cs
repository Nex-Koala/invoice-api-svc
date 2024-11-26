using AutoMapper;
using FluentValidation;
using invoice_api_svc.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace invoice_api_svc.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceExtensions).Assembly);
            services.AddAutoMapper(typeof(ServiceExtensions).Assembly);
            services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
