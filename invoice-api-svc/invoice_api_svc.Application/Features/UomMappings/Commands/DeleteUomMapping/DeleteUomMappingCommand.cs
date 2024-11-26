using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.UomMappings.Commands.DeleteUomMapping
{
    public class DeleteUomMappingCommand : IRequest<Response<int>>
    {
        public int Id { get; set; } // ID of the UOM Mapping to delete
        public class DeleteUomMappingCommandHandler : IRequestHandler<DeleteUomMappingCommand, Response<int>>
        {
            private readonly IUomMappingRepositoryAsync _uomMappingRepository;

            public DeleteUomMappingCommandHandler(IUomMappingRepositoryAsync uomMappingRepository)
            {
                _uomMappingRepository = uomMappingRepository;
            }

            public async Task<Response<int>> Handle(DeleteUomMappingCommand request, CancellationToken cancellationToken)
            {
                var existingMapping = await _uomMappingRepository.GetByIdAsync(request.Id);
                if (existingMapping == null)
                {
                    return new Response<int>($"Mapping with ID {request.Id} not found.");
                }

                await _uomMappingRepository.DeleteAsync(existingMapping);
                return new Response<int>(request.Id, "Mapping deleted successfully.");
            }
        }
    }
}
