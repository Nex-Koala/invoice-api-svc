using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using AutoMapper;
using invoice_api_svc.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Partners.Commands.CreatePartner
{
    public class CreatePartnerCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LicenseKey { get; set; }
        public bool Status { get; set; }
        public int MaxSubmissions { get; set; }
    }
    public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public CreatePartnerCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            await _userRepository.AddAsync(user);
            return new Response<int>(user.Id);
        }
    }
}
