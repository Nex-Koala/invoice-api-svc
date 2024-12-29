using AutoMapper;
using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Features.Partners.Queries.GetAllPartners;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using invoice_api_svc.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Partners.Queries.GetPartnerById
{
    public class GetPartnerByIdQuery : IRequest<Response<PartnersViewModel>>
    {
        public int Id { get; set; }
        public class GetPartnerByIdQueryHandler : IRequestHandler<GetPartnerByIdQuery, Response<PartnersViewModel>>
        {
            private readonly IUserRepositoryAsync _userRepository;
            private readonly IMapper _mapper;
            public GetPartnerByIdQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }
            public async Task<Response<PartnersViewModel>> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user == null) throw new Exception($"Partner with ID {request.Id} Not Found.");

                var viewModel = _mapper.Map<PartnersViewModel>(user);

                return new Response<PartnersViewModel>(viewModel);
            }
        }
    }
}
