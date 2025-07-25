using Mapster;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.CreateInvoice.v1;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Mappings;

public class GeneralMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RawDocumentJson, RawDocument>()
            .Ignore(dest => dest.Document);

        config.NewConfig<CreateInvoiceCommand, InvoiceDocument>()
            .Map(dest => dest.Supplier, src => new Supplier { Name = src.SupplierName });

        config.NewConfig<InvoiceLineDto, InvoiceLine>();

        config
            .NewConfig<InvoiceDocument, Features.InvoiceDocuments.Get.v1.InvoiceDocumentResponse>()
            .Map(dest => dest.DocumentStatus, src => src.DocumentStatus.HasValue ? src.DocumentStatus.Value.ToString() : null);

        config.NewConfig<LicenseKey, LicenseKeyResponseDto>();
        config.NewConfig<Partner, PartnerResponse>()
            .Map(dest => dest.LicenseKey, src => src.LicenseKey.Adapt<LicenseKeyResponseDto>());
    }
}
