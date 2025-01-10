using AutoMapper;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace invoice_api_svc.Application.Features.ClassificationMapping.Queries.GetAllClassificationMappings
{
    public class GetAllClassificationMappingsQuery : IRequest<Response<IEnumerable<ClassificationMappingViewModel>>>
    {
        public Guid? UserId { get; set; }
    }

    public class GetAllClassificationMappingsQueryHandler : IRequestHandler<GetAllClassificationMappingsQuery, Response<IEnumerable<ClassificationMappingViewModel>>>
    {
        private readonly IClassificationMappingRepositoryAsync _classificationMappingRepository;
        private readonly IMapper _mapper;

        public GetAllClassificationMappingsQueryHandler(IClassificationMappingRepositoryAsync classificationMappingRepository, IMapper mapper)
        {
            _classificationMappingRepository = classificationMappingRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ClassificationMappingViewModel>>> Handle(GetAllClassificationMappingsQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Domain.Entities.ClassificationMapping> mappings;
            if (request?.UserId != null)
            {
                mappings = await _classificationMappingRepository.GetMappingsByUserIdAsync((Guid)request.UserId);
            }
            else
            {
                mappings = await _classificationMappingRepository.GetAllAsync();
            }

            var mappingViewModels = _mapper.Map<IEnumerable<ClassificationMappingViewModel>>(mappings);

            return new Response<IEnumerable<ClassificationMappingViewModel>>(mappingViewModels);
        }
    }
}
