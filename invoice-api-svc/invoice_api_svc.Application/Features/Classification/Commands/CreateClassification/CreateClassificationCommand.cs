using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using invoice_api_svc.Domain.Entities;

namespace invoice_api_svc.Application.Features.Classification.Commands.CreateClassification
{
    public class CreateClassificationCommand : IRequest<Response<int>>
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }

        public class CreateClassificationCommandHandler : IRequestHandler<CreateClassificationCommand, Response<int>>
        {
            private readonly IClassificationRepositoryAsync _classificationRepository;

            public CreateClassificationCommandHandler(IClassificationRepositoryAsync classificationRepository)
            {
                _classificationRepository = classificationRepository;
            }

            public async Task<Response<int>> Handle(CreateClassificationCommand request, CancellationToken cancellationToken)
            {
                var newClassification = new Domain.Entities.Classification
                {
                    Code = request.Code,
                    Description = request.Description,
                    UserId = request.UserId
                };

                var createdClassification = await _classificationRepository.AddAsync(newClassification);
                return new Response<int>(createdClassification.Id);
            }
        }
    }
}
