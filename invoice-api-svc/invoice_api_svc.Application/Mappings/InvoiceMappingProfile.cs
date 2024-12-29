using AutoMapper;
using invoice_api_svc.Application.DTOs.EInvoice;
using invoice_api_svc.Application.Features.InvoiceDocuments.Commands.CreateInvoice;
using invoice_api_svc.Domain.Entities;

namespace invoice_api_svc.Application.Mappings
{
    public class InvoiceMappingProfile : Profile
    {
        public InvoiceMappingProfile()
        {
            CreateMap<CreateInvoiceCommand, InvoiceDocument>()
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => new Supplier { Name = src.SupplierName }));
            CreateMap<InvoiceLineDto, InvoiceLine>();
        }
    }
}
