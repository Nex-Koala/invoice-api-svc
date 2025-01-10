using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Classification.Commands.UpdateClassification
{
    public class UpdateClassificationCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public class UpdateClassificationCommandHandler : IRequestHandler<UpdateClassificationCommand, Response<int>>
        {
            private readonly IClassificationRepositoryAsync _classificationRepository;

            public UpdateClassificationCommandHandler(IClassificationRepositoryAsync classificationRepository)
            {
                _classificationRepository = classificationRepository;
            }

            public async Task<Response<int>> Handle(UpdateClassificationCommand request, CancellationToken cancellationToken)
            {
                var classification = await _classificationRepository.GetByIdAsync(request.Id);
                if (classification == null)
                {
                    throw new Exception($"Classification with Id {request.Id} not found.");
                }

                classification.Code = request.Code;
                classification.Description = request.Description;

                await _classificationRepository.UpdateAsync(classification);

                return new Response<int>(classification.Id);
            }
        }
    }
}
