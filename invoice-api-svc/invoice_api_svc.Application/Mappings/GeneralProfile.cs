using invoice_api_svc.Application.Features.Products.Commands.CreateProduct;
using invoice_api_svc.Application.Features.Products.Queries.GetAllProducts;
using AutoMapper;
using invoice_api_svc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace invoice_api_svc.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
