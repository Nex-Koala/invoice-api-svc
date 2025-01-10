using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using invoice_api_svc.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.ClassificationMapping.Commands.CreateClassificationMapping
{
    public class CreateClassificationMappingCommand : IRequest<Response<int>>
    {
        public int? Id { get; set; }
        public string LhdnClassificationCode { get; set; } = default!;
        public int ClassificationId { get; set; }
    }

    public class CreateClassificationMappingCommandHandler : IRequestHandler<CreateClassificationMappingCommand, Response<int>>
    {
        private readonly IClassificationMappingRepositoryAsync _classificationMappingRepository;
        private readonly IClassificationRepositoryAsync _classificationRepository;

        public CreateClassificationMappingCommandHandler(
            IClassificationMappingRepositoryAsync classificationMappingRepository,
            IClassificationRepositoryAsync classificationRepository)
        {
            _classificationMappingRepository = classificationMappingRepository;
            _classificationRepository = classificationRepository;
        }

        public async Task<Response<int>> Handle(CreateClassificationMappingCommand request, CancellationToken cancellationToken)
        {
            // Step 1: Validate Classification exists
            var classificationExists = await _classificationRepository.GetByIdAsync(request.ClassificationId);
            if (classificationExists == null)
            {
                return new Response<int>($"Classification with ID {request.ClassificationId} does not exist.");
            }

            if (request.Id.HasValue)
            {
                // Step 2: Update an existing mapping
                var existingMapping = await _classificationMappingRepository.GetByIdAsync(request.Id.Value);
                if (existingMapping == null)
                {
                    return new Response<int>($"Mapping with ID {request.Id.Value} not found.");
                }

                // Validate: Ensure Classification isn't mapped to another LHDN code
                var duplicateMapping = await _classificationMappingRepository.GetByConditionAsync(x =>
                    x.ClassificationId == request.ClassificationId && x.LhdnClassificationCode != request.LhdnClassificationCode && !x.IsDeleted);

                if (duplicateMapping != null)
                {
                    return new Response<int>("This Classification is already mapped to another LHDN Classification Code.");
                }

                // Update fields
                existingMapping.LhdnClassificationCode = request.LhdnClassificationCode;
                existingMapping.ClassificationId = request.ClassificationId;

                await _classificationMappingRepository.UpdateAsync(existingMapping);
                return new Response<int>(existingMapping.Id, "Mapping updated successfully.");
            }
            else
            {
                // Step 3: Create a new mapping
                // Validate: Ensure the Classification isn't already mapped to this LHDN code
                var existingMapping = await _classificationMappingRepository.GetByConditionAsync(x =>
                    x.ClassificationId == request.ClassificationId && x.LhdnClassificationCode == request.LhdnClassificationCode && !x.IsDeleted);

                if (existingMapping != null)
                {
                    return new Response<int>("This Classification is already mapped to the specified LHDN Classification Code.");
                }

                var newMapping = new Domain.Entities.ClassificationMapping
                {
                    LhdnClassificationCode = request.LhdnClassificationCode,
                    ClassificationId = request.ClassificationId
                };

                var createdMapping = await _classificationMappingRepository.AddAsync(newMapping);
                return new Response<int>(createdMapping.Id, "Mapping created successfully.");
            }
        }
    }
}
