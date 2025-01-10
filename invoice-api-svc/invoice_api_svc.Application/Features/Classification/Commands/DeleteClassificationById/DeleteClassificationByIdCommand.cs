using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Classification.Commands.DeleteClassificationById
{
    public class DeleteClassificationByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteClassificationByIdCommandHandler : IRequestHandler<DeleteClassificationByIdCommand, Response<int>>
        {
            private readonly IClassificationRepositoryAsync _classificationRepository;

            public DeleteClassificationByIdCommandHandler(IClassificationRepositoryAsync classificationRepository)
            {
                _classificationRepository = classificationRepository;
            }

            public async Task<Response<int>> Handle(DeleteClassificationByIdCommand request, CancellationToken cancellationToken)
            {
                var classification = await _classificationRepository.GetByIdAsync(request.Id);
                if (classification == null)
                {
                    throw new Exception($"Classification with Id {request.Id} not found.");
                }

                await _classificationRepository.DeleteAsync(classification);

                return new Response<int>(request.Id);
            }
        }
    }
}
