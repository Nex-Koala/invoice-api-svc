using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Infrastructure.Persistence;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Settings;
using NexKoala.WebApi.Invoice.Infrastructure.Apis;
using NexKoala.WebApi.Invoice.Infrastructure.Audit;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Classification;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.ClassificationMapping;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Partner;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Profile;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Statistic;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Supplier;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Uom;
using NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.UomMapping;
using NexKoala.WebApi.Invoice.Infrastructure.Jobs;
using NexKoala.WebApi.Invoice.Infrastructure.Persistence;
using NexKoala.WebApi.Invoice.Infrastructure.Services;

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
            invoiceApiGroup.MapGetMsicCodesEndpoint();
            invoiceApiGroup.MapGetStateCodesEndpoint();
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
            invoiceApiGroup.MapGenerateInvoiceFromDbEndpoint();
            invoiceApiGroup.MapGetSalesInvoicesEndpoint();
            invoiceApiGroup.MapGetPurchaseInvoicesEndpoint();
            invoiceApiGroup.MapGetCreditDebitNotesEndpoint();
            invoiceApiGroup.MapGetPurchaseCreditDebitNotesEndpoint();
            invoiceApiGroup.MapGetInvoiceDocumentEndpoint();
            invoiceApiGroup.MapGetInvoiceDocumentListEndpoint();
            invoiceApiGroup.MapExportInvoiceSubmissionExcelEndpoint();

            var partnerGroup = app.MapGroup("partners").WithTags("Partners");
            partnerGroup.MapPartnerCreationEndpoint();
            partnerGroup.MapPartnerDeleteEndpoint();
            partnerGroup.MapPartnerUpdateEndpoint();
            partnerGroup.MapGetPartnerEndpoint();
            partnerGroup.MapGetPartnerListEndpoint();

            var profileGroup = app.MapGroup("profile").WithTags("Profile");
            profileGroup.MapUpdateProfileEndpoint();
            profileGroup.MapGetProfileEndpoint();

            var dashboardGroup = app.MapGroup("dashboard").WithTags("Dashboard");
            dashboardGroup.MapGetSageSubmissionRateEndpoint();
            dashboardGroup.MapGetLhdnSubmissionRateEndpoint();

            var supplierGroup = app.MapGroup("suppliers").WithTags("Suppliers");
            supplierGroup.MapGetAllSupplierEndpoint();
        }
    }

    public static WebApplicationBuilder RegisterInvoiceServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<InvoiceDbContext>();
        builder.Services.AddScoped<IDbInitializer, InvoiceDbInitializer>();

        builder.Services.AddDbContext<ClientDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("ClientConnection"),
            b => b.MigrationsAssembly(typeof(ClientDbContext).Assembly.FullName))
        );

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

        builder.Services.AddKeyedScoped<IRepository<InvoiceLine>, InvoiceRepository<InvoiceLine>>("invoice:invoiceLines");
        builder.Services.AddKeyedScoped<IReadRepository<InvoiceLine>, InvoiceRepository<InvoiceLine>>("invoice:invoiceLines");

        builder.Services.AddKeyedScoped<IRepository<Supplier>, InvoiceRepository<Supplier>>("invoice:suppliers");
        builder.Services.AddKeyedScoped<IReadRepository<Supplier>, InvoiceRepository<Supplier>>("invoice:suppliers");

        builder.Services.AddKeyedScoped<IRepository<Customer>, InvoiceRepository<Customer>>("invoice:customers");
        builder.Services.AddKeyedScoped<IReadRepository<Customer>, InvoiceRepository<Customer>>("invoice:customers");

        builder.Services.AddKeyedScoped<IRepository<LicenseKey>, InvoiceRepository<LicenseKey>>("invoice:licenseKeys");
        builder.Services.AddKeyedScoped<IReadRepository<LicenseKey>, InvoiceRepository<LicenseKey>>("invoice:licenseKeys");

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
        builder.Services.AddScoped<TrimStringService>();
        builder.Services.AddScoped<IAuditService, AuditService>();
        builder.Services.AddSingleton<IMsicService, MsicService>();

        builder.Services.AddInvoiceJobs(builder.Configuration);

        return builder;
    }

    public static WebApplication UseInvoiceModule(this WebApplication app)
    {
        return app;
    }
}
