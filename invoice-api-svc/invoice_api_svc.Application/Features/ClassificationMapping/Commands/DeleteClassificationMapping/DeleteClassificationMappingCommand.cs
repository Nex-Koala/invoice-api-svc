using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.ClassificationMapping.Commands.DeleteClassificationMapping
{
    public class DeleteClassificationMappingCommand : IRequest<Response<int>>
    {
        public int Id { get; set; } // ID of the Classification Mapping to delete
        public class DeleteClassificationMappingCommandHandler : IRequestHandler<DeleteClassificationMappingCommand, Response<int>>
        {
            private readonly IClassificationMappingRepositoryAsync _classificationMappingRepository;

            public DeleteClassificationMappingCommandHandler(IClassificationMappingRepositoryAsync classificationMappingRepository)
            {
                _classificationMappingRepository = classificationMappingRepository;
            }

            public async Task<Response<int>> Handle(DeleteClassificationMappingCommand request, CancellationToken cancellationToken)
            {
                var existingMapping = await _classificationMappingRepository.GetByIdAsync(request.Id);
                if (existingMapping == null)
                {
                    return new Response<int>($"Mapping with ID {request.Id} not found.");
                }

                await _classificationMappingRepository.DeleteAsync(existingMapping);
                return new Response<int>(request.Id, "Mapping deleted successfully.");
            }
        }
    }
}
