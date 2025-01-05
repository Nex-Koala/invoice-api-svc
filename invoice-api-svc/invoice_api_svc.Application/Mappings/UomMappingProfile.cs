using AutoMapper;
using invoice_api_svc.Application.Features.UomMappings.Queries.GetAllUomMappings;
using invoice_api_svc.Application.Features.Uoms.Queries.GetAllUoms;
using invoice_api_svc.Domain.Entities;

namespace invoice_api_svc.Application.Mappings
{
    public class UomMappingProfile : Profile
    {
        public UomMappingProfile()
        {
            CreateMap<GetAllUomsQuery, GetAllUomsParameter>();
            CreateMap<Uom, GetAllUomsViewModel>();
            CreateMap<UomMapping, UomMappingViewModel>();
        }
    }
}
