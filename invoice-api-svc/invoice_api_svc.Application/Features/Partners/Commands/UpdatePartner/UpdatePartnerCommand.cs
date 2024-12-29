using AutoMapper;
using invoice_api_svc.Application.DTOs.User;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Partners.Commands.UpdatePartner
{
    public class UpdatePartnerCommand : IRequest<Response<int>>
    {
        public bool IsAdmin { get; set; }
        public object UpdateDto { get; set; }

        public class UpdatePartnerCommandHandler : IRequestHandler<UpdatePartnerCommand, Response<int>>
        {
            private readonly IUserRepositoryAsync _userRepository;
            private readonly IMapper _mapper;
            public UpdatePartnerCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }
            public async Task<Response<int>> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
            {
                int partnerId = 0;
                if (request.IsAdmin)
                {
                    var adminDto = request.UpdateDto as AdminUpdatePartnerDto;
                    if (adminDto != null)
                    {
                        partnerId = adminDto.Id;
                    }
                }
                else
                {
                    var userDto = request.UpdateDto as UserUpdateProfileDto;
                    if (userDto != null)
                    {
                        partnerId = userDto.Id;
                    }
                }

                var user = await _userRepository.GetByIdAsync(partnerId);

                if (user == null)
                {
                    throw new Exception($"Partner with ID {partnerId} Not Found.");
                }
                else
                {
                    if (request.IsAdmin)
                    {
                        var adminDto = request.UpdateDto as AdminUpdatePartnerDto;
                        _mapper.Map(adminDto, user);
                    }
                    else
                    {
                        var userDto = request.UpdateDto as UserUpdateProfileDto;
                        _mapper.Map(userDto, user);
                    }

                    await _userRepository.UpdateAsync(user);
                    return new Response<int>(user.Id);
                }
            }
        }
    }
}
