using invoice_api_svc.Application.Features.Products.Commands.CreateProduct;
using invoice_api_svc.Application.Features.Products.Queries.GetAllProducts;
using AutoMapper;
using invoice_api_svc.Domain.Entities;
using invoice_api_svc.Application.DTOs.EInvoice.Document;

namespace invoice_api_svc.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
            CreateMap<RawDocumentJson, RawDocument>().ForMember(dest => dest.Document, opt => opt.Ignore());
        }
    }
}
