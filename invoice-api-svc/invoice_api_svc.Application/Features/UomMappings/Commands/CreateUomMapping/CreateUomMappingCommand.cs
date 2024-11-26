using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using invoice_api_svc.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.UomMappings.Commands.CreateUomMapping
{
    public class CreateUomMappingCommand : IRequest<Response<int>>
    {
        public int? Id { get; set; } // Nullable for create or update distinction
        public string LhdnUomCode { get; set; } = default!;
        public int UomId { get; set; } // Foreign key to the UOM entity
    }

    public class CreateUomMappingCommandHandler : IRequestHandler<CreateUomMappingCommand, Response<int>>
    {
        private readonly IUomMappingRepositoryAsync _uomMappingRepository;
        private readonly IUomRepositoryAsync _uomRepository;

        public CreateUomMappingCommandHandler(
            IUomMappingRepositoryAsync uomMappingRepository,
            IUomRepositoryAsync uomRepository)
        {
            _uomMappingRepository = uomMappingRepository;
            _uomRepository = uomRepository;
        }

        public async Task<Response<int>> Handle(CreateUomMappingCommand request, CancellationToken cancellationToken)
        {
            // Step 1: Validate UOM exists
            var uomExists = await _uomRepository.GetByIdAsync(request.UomId);
            if (uomExists == null)
            {
                return new Response<int>($"UOM with ID {request.UomId} does not exist.");
            }

            if (request.Id.HasValue)
            {
                // Step 2: Update an existing mapping
                var existingMapping = await _uomMappingRepository.GetByIdAsync(request.Id.Value);
                if (existingMapping == null)
                {
                    return new Response<int>($"Mapping with ID {request.Id.Value} not found.");
                }

                // Validate: Ensure UOM isn't mapped to another LHDN code
                var duplicateMapping = await _uomMappingRepository.GetByConditionAsync(x =>
                    x.UomId == request.UomId && x.LhdnUomCode != request.LhdnUomCode && !x.IsDeleted);

                if (duplicateMapping != null)
                {
                    return new Response<int>("This UOM is already mapped to another LHDN UOM Code.");
                }

                // Update fields
                existingMapping.LhdnUomCode = request.LhdnUomCode;
                existingMapping.UomId = request.UomId;

                await _uomMappingRepository.UpdateAsync(existingMapping);
                return new Response<int>(existingMapping.Id, "Mapping updated successfully.");
            }
            else
            {
                // Step 3: Create a new mapping
                // Validate: Ensure the UOM isn't already mapped to this LHDN code
                var existingMapping = await _uomMappingRepository.GetByConditionAsync(x =>
                    x.UomId == request.UomId && x.LhdnUomCode == request.LhdnUomCode && !x.IsDeleted);

                if (existingMapping != null)
                {
                    return new Response<int>("This UOM is already mapped to the specified LHDN UOM Code.");
                }

                var newMapping = new UomMapping
                {
                    LhdnUomCode = request.LhdnUomCode,
                    UomId = request.UomId
                };

                var createdMapping = await _uomMappingRepository.AddAsync(newMapping);
                return new Response<int>(createdMapping.Id, "Mapping created successfully.");
            }
        }
    }
}
