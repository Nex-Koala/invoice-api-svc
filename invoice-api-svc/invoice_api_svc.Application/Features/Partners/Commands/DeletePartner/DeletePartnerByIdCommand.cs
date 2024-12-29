using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Partners.Commands.DeletePartner
{
    public class DeletePartnerByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeletePartnerByIdCommandHandler : IRequestHandler<DeletePartnerByIdCommand, Response<int>>
        {
            private readonly IUserRepositoryAsync _userRepository;
            public DeletePartnerByIdCommandHandler(IUserRepositoryAsync userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<Response<int>> Handle(DeletePartnerByIdCommand request, CancellationToken cancellationToken)
            {
                var existingUser = await _userRepository.GetByIdAsync(request.Id);
                if (existingUser == null)
                {
                    throw new Exception($"Partner with ID {request.Id} not found.");
                }
                await _userRepository.DeleteAsync(existingUser);
                return new Response<int>(existingUser.Id);
            }
        }
    }
}
