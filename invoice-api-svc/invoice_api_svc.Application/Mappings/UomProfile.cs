using AutoMapper;
using invoice_api_svc.Application.Features.Products.Queries.GetAllUoms;
using invoice_api_svc.Domain.Entities;

namespace invoice_api_svc.Application.Mappings
{
    public class UomProfile : Profile
    {
        public UomProfile()
        {
            CreateMap<GetAllUomsQuery, GetAllUomsParameter>();
            CreateMap<Uom, GetAllUomsViewModel>();
        }
    }
}
