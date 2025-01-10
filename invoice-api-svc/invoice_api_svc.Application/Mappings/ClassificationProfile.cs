using AutoMapper;
using invoice_api_svc.Application.Features.Classification.Queries.GetAllClassifications;
using invoice_api_svc.Domain.Entities;

namespace invoice_api_svc.Application.Mappings
{
    public class ClassificationProfile : Profile
    {
        public ClassificationProfile()
        {
            // Map GetAllClassificationsQuery to GetAllClassificationsParameter
            CreateMap<GetAllClassificationQuery, GetAllClassificationParameter>();

            // Map Classification entity to GetAllClassificationsViewModel
            CreateMap<Classification, GetAllClassificationViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            // Enable reverse mapping if needed
            CreateMap<Classification, GetAllClassificationViewModel>().ReverseMap();
        }
    }
}
