using AutoMapper;
using invoice_api_svc.Application.Features.ClassificationMapping.Queries.GetAllClassificationMappings;
using invoice_api_svc.Domain.Entities;

namespace invoice_api_svc.Application.Mappings
{
    public class ClassificationMappingProfile : Profile
    {
        public ClassificationMappingProfile()
        {
            CreateMap<Classification, ClassificationMappingViewModel>();
            CreateMap<ClassificationMapping, ClassificationMappingViewModel>();
        }
    }
}
