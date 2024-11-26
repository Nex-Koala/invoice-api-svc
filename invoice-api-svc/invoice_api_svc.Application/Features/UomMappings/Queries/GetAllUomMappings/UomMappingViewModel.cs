using AutoMapper;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using invoice_api_svc.Application.Interfaces.Repositories;

namespace invoice_api_svc.Application.Features.UomMappings.Queries.GetAllUomMappings
{
    public class UomMappingViewModel
    {
        public int Id { get; set; }
        public string LhdnUomCode { get; set; } = default!;
        public int UomId { get; set; }

        public class GetAllUomMappingsQueryHandler : IRequestHandler<GetAllUomMappingsQuery, Response<IEnumerable<UomMappingViewModel>>>
        {
            private readonly IUomMappingRepositoryAsync _uomMappingRepository;
            private readonly IMapper _mapper;

            public GetAllUomMappingsQueryHandler(IUomMappingRepositoryAsync uomMappingRepository, IMapper mapper)
            {
                _uomMappingRepository = uomMappingRepository;
                _mapper = mapper;
            }

            public async Task<Response<IEnumerable<UomMappingViewModel>>> Handle(GetAllUomMappingsQuery request, CancellationToken cancellationToken)
            {
                var mappings = await _uomMappingRepository.GetAllAsync();
                var mappingViewModels = _mapper.Map<IEnumerable<UomMappingViewModel>>(mappings);

                return new Response<IEnumerable<UomMappingViewModel>>(mappingViewModels);
            }
        }
    }
}
