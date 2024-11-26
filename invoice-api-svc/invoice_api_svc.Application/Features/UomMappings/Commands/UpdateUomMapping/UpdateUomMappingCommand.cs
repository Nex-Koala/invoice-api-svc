using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using invoice_api_svc.Application.Interfaces.Repositories;

namespace invoice_api_svc.Application.Features.UomMappings.Commands.UpdateUomMapping
{
    public class UpdateUomMappingCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string LhdnUomCode { get; set; } = default!;
        public int UomId { get; set; }

        public class UpdateUomMappingCommandHandler : IRequestHandler<UpdateUomMappingCommand, Response<int>>
        {
            private readonly IUomMappingRepositoryAsync _uomMappingRepository;

            public UpdateUomMappingCommandHandler(IUomMappingRepositoryAsync uomMappingRepository)
            {
                _uomMappingRepository = uomMappingRepository;
            }

            public async Task<Response<int>> Handle(UpdateUomMappingCommand request, CancellationToken cancellationToken)
            {
                var mapping = await _uomMappingRepository.GetByIdAsync(request.Id);
                if (mapping == null)
                {
                    throw new System.Exception($"UOM Mapping with ID {request.Id} not found.");
                }

                mapping.LhdnUomCode = request.LhdnUomCode;
                mapping.UomId = request.UomId;

                await _uomMappingRepository.UpdateAsync(mapping);

                return new Response<int>(mapping.Id);
            }
        }
    }
}
