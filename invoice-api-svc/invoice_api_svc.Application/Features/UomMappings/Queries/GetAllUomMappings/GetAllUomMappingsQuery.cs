using AutoMapper;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using invoice_api_svc.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace invoice_api_svc.Application.Features.UomMappings.Queries.GetAllUomMappings
{
    public class GetAllUomMappingsQuery : IRequest<Response<IEnumerable<UomMappingViewModel>>>
    {
        public Guid? UserId { get; set; }
    }

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
            IReadOnlyList<UomMapping> mappings;
            if (request?.UserId != null)
            {
                mappings = await _uomMappingRepository.GetMappingsByUserIdAsync((Guid)request.UserId);
            }
            else
            {
                mappings = await _uomMappingRepository.GetAllAsync();
            }

            var mappingViewModels = _mapper.Map<IEnumerable<UomMappingViewModel>>(mappings);

            return new Response<IEnumerable<UomMappingViewModel>>(mappingViewModels);
        }
    }
}
