using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using invoice_api_svc.Application.Interfaces.Repositories;

namespace invoice_api_svc.Application.Features.ClassificationMapping.Commands.UpdateClassificationMapping
{
    public class UpdateClassificationMappingCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string LhdnClassificationCode { get; set; } = default!;
        public int ClassificationId { get; set; }

        public class UpdateClassificationMappingCommandHandler : IRequestHandler<UpdateClassificationMappingCommand, Response<int>>
        {
            private readonly IClassificationMappingRepositoryAsync _classificationMappingRepository;

            public UpdateClassificationMappingCommandHandler(IClassificationMappingRepositoryAsync classificationMappingRepository)
            {
                _classificationMappingRepository = classificationMappingRepository;
            }

            public async Task<Response<int>> Handle(UpdateClassificationMappingCommand request, CancellationToken cancellationToken)
            {
                var existingMapping = await _classificationMappingRepository.GetByIdAsync(request.Id);
                if (existingMapping == null)
                {
                    return new Response<int>($"Mapping with ID {request.Id} not found.");
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
        }
    }
}
