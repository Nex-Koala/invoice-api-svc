using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Infrastructure.Persistence;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Settings;
using NexKoala.WebApi.Invoice.Infrastructure.Apis;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Classification;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.ClassificationMapping;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Partner;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Profile;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Uom;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.UomMapping;
using NexKoala.WebApi.Invoice.Infrastructure.Persistence;
using NexKoala.WebApi.Invoice.Infrastructure.Services;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace NexKoala.WebApi.Invoice.Infrastructure;

public static class InvoiceModule
{
    public class Endpoints : CarterModule
    {
        public Endpoints()
            : base("") { }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var uomGroup = app.MapGroup("uoms").WithTags("UOM");
            uomGroup.MapUomCreationEndpoint();
            uomGroup.MapUomUpdateEndpoint();
            uomGroup.MapUomDeleteEndpoint();
            uomGroup.MapGetUomEndpoint();
            uomGroup.MapGetUomListEndpoint();

            var uomMappingGroup = app.MapGroup("uom-mappings").WithTags("UOM Mapping");
            uomMappingGroup.MapUomMappingCreationEndpoint();
            uomMappingGroup.MapUomMappingUpdateEndpoint();
            uomMappingGroup.MapUomMappingDeleteEndpoint();
            uomMappingGroup.MapGetUomMappingEndpoint();
            uomMappingGroup.MapGetUomMappingListEndpoint();

            var classificationGroup = app.MapGroup("classifications").WithTags("Classification");
            classificationGroup.MapClassificationCreationEndpoint();
            classificationGroup.MapClassificationUpdateEndpoint();
            classificationGroup.MapClassificationDeleteEndpoint();
            classificationGroup.MapGetClassificationEndpoint();
            classificationGroup.MapGetClassificationListEndpoint();

            var classificationMappingGroup = app.MapGroup("classification-mappings").WithTags("Classification Mapping");
            classificationMappingGroup.MapClassificationMappingCreationEndpoint();
            classificationMappingGroup.MapClassificationMappingUpdateEndpoint();
            classificationMappingGroup.MapClassificationMappingDeleteEndpoint();
            classificationMappingGroup.MapGetClassificationMappingEndpoint();
            classificationMappingGroup.MapGetClassificationMappingListEndpoint();

            var invoiceApiGroup = app.MapGroup("invoiceApi").WithTags("Invoice Api");
            invoiceApiGroup.MapGetClassificationCodesEndpoint();
            invoiceApiGroup.MapGetTaxTypesEndpoint();
            invoiceApiGroup.MapGetCurrencyCodesEndpoint();
            invoiceApiGroup.MapGetInvoiceTypesEndpoint();
            invoiceApiGroup.MapGetUnitTypesEndpoint();
            invoiceApiGroup.MapGetRawDocumentEndpoint();
            invoiceApiGroup.MapGetDocumentDetailsEndpoint();
            invoiceApiGroup.MapGetRecentDocumentsEndpoint();
            invoiceApiGroup.MapCreateInvoiceEndpoint();
            invoiceApiGroup.MapSubmitInvoiceEndpoint();
            invoiceApiGroup.MapGenerateInvoiceEndpoint();

            var partnerGroup = app.MapGroup("partners").WithTags("Partners");
            partnerGroup.MapPartnerCreationEndpoint();
            partnerGroup.MapPartnerDeleteEndpoint();
            partnerGroup.MapPartnerUpdateEndpoint();
            partnerGroup.MapGetPartnerEndpoint();
            partnerGroup.MapGetPartnerListEndpoint();

            var profileGroup = app.MapGroup("profile").WithTags("Profile");
            profileGroup.MapUpdateProfileEndpoint();
            profileGroup.MapGetProfileEndpoint();
        }
    }

    public static WebApplicationBuilder RegisterInvoiceServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<InvoiceDbContext>();
        builder.Services.AddScoped<IDbInitializer, InvoiceDbInitializer>();

        builder.Services.AddKeyedScoped<IRepository<Uom>, InvoiceRepository<Uom>>("invoice:uoms");
        builder.Services.AddKeyedScoped<IReadRepository<Uom>, InvoiceRepository<Uom>>("invoice:uoms");

        builder.Services.AddKeyedScoped<IRepository<UomMapping>, InvoiceRepository<UomMapping>>("invoice:uomMappings");
        builder.Services.AddKeyedScoped<IReadRepository<UomMapping>, InvoiceRepository<UomMapping>>("invoice:uomMappings");

        builder.Services.AddKeyedScoped<IRepository<Classification>, InvoiceRepository<Classification>>("invoice:classifications");
        builder.Services.AddKeyedScoped<IReadRepository<Classification>, InvoiceRepository<Classification>>("invoice:classifications");

        builder.Services.AddKeyedScoped<IRepository<ClassificationMapping>, InvoiceRepository<ClassificationMapping>>("invoice:classificationMappings");
        builder.Services.AddKeyedScoped<IReadRepository<ClassificationMapping>, InvoiceRepository<ClassificationMapping>>("invoice:classificationMappings");

        builder.Services.AddKeyedScoped<IRepository<InvoiceDocument>, InvoiceRepository<InvoiceDocument>>("invoice:invoiceDocuments");
        builder.Services.AddKeyedScoped<IReadRepository<InvoiceDocument>, InvoiceRepository<InvoiceDocument>>("invoice:invoiceDocuments");

        builder.Services.AddKeyedScoped<IRepository<Partner>, InvoiceRepository<Partner>>("invoice:partners");
        builder.Services.AddKeyedScoped<IReadRepository<Partner>, InvoiceRepository<Partner>>("invoice:partners");

        builder.Services.AddHttpClient("LhdnApi", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration.GetSection("EInvoiceSettings:ApiBaseUrl").Value);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddHttpClient("LhdnSdk", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration.GetSection("EInvoiceSettings:SdkBaseUrl").Value);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(30);
        });
        builder.Services.Configure<EInvoiceSettings>(builder.Configuration.GetSection("EInvoiceSettings"));
        builder.Services.AddScoped<IInvoiceService, InvoiceService>();
        builder.Services.AddScoped<ILhdnApi, LhdnApi>();
        builder.Services.AddScoped<ILhdnSdk, LhdnSdk>();
        builder.Services.AddScoped<IQuotaService, QuotaService>();

        return builder;
    }

    public static WebApplication UseInvoiceModule(this WebApplication app)
    {
        return app;
    }
}
