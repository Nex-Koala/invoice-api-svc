using AutoMapper;
using invoice_api_svc.Application.Features.Uoms.Queries.GetAllUoms;
using invoice_api_svc.Domain.Entities;

namespace invoice_api_svc.Application.Mappings
{
    public class UomProfile : Profile
    {
        public UomProfile()
        {
            // Map GetAllUomsQuery to GetAllUomsParameter
            CreateMap<GetAllUomsQuery, GetAllUomsParameter>();

            // Map Uom entity to GetAllUomsViewModel
            CreateMap<Uom, GetAllUomsViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            // Enable reverse mapping if needed
            CreateMap<Uom, GetAllUomsViewModel>().ReverseMap();
        }
    }
}
