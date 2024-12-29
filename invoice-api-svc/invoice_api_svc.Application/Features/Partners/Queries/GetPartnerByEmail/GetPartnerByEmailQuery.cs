using AutoMapper;
using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Features.Partners.Queries.GetAllPartners;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Partners.Queries.GetPartnerByEmail
{
    public class GetPartnerByEmailQuery : IRequest<Response<PartnersViewModel>>
    {
        public string Email { get; set; }
        public class GetPartnerByEmailQueryHandler : IRequestHandler<GetPartnerByEmailQuery, Response<PartnersViewModel>>
        {
            private readonly IUserRepositoryAsync _userRepository;
            private readonly IMapper _mapper;
            public GetPartnerByEmailQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }
            public async Task<Response<PartnersViewModel>> Handle(GetPartnerByEmailQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByEmailAsync(request.Email);

                if (user == null) throw new Exception($"Partner with email {request.Email} Not Found.");

                var viewModel = _mapper.Map<PartnersViewModel>(user);

                return new Response<PartnersViewModel>(viewModel);
            }
        }
    }
}
