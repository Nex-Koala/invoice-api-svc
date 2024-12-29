using AutoMapper;
using invoice_api_svc.Application.DTOs.User;
using invoice_api_svc.Application.Features.Partners.Commands.CreatePartner;
using invoice_api_svc.Application.Features.Partners.Queries.GetAllPartners;
using invoice_api_svc.Domain.Entities;

namespace invoice_api_svc.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, PartnersViewModel>();
            CreateMap<CreatePartnerCommand, User>();
            CreateMap<AdminUpdatePartnerDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserUpdateProfileDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<GetAllPartnersQuery, GetAllPartnerFilter>();
        }
    }
}
