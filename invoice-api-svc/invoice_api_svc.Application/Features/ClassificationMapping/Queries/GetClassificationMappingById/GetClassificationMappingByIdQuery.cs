using AutoMapper;
using invoice_api_svc.Application.Features.ClassificationMapping.Queries.GetAllClassificationMappings;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.ClassificationMapping.Queries.GetClassificationMappingById
{
    public class GetClassificationMappingByIdQuery : IRequest<Response<ClassificationMappingViewModel>>
    {
        public int Id { get; set; }

        public class GetClassificationMappingByIdQueryHandler : IRequestHandler<GetClassificationMappingByIdQuery, Response<ClassificationMappingViewModel>>
        {
            private readonly IClassificationMappingRepositoryAsync _classificationMappingRepository;
            private readonly IMapper _mapper;

            public GetClassificationMappingByIdQueryHandler(IClassificationMappingRepositoryAsync classificationMappingRepository, IMapper mapper)
            {
                _classificationMappingRepository = classificationMappingRepository;
                _mapper = mapper;
            }

            public async Task<Response<ClassificationMappingViewModel>> Handle(GetClassificationMappingByIdQuery request, CancellationToken cancellationToken)
            {
                var classificationMapping = await _classificationMappingRepository.GetByIdAsync(request.Id);

                if (classificationMapping == null)
                {
                    return new Response<ClassificationMappingViewModel>("Classification Mapping not found");
                }

                var viewModel = _mapper.Map<ClassificationMappingViewModel>(classificationMapping);
                return new Response<ClassificationMappingViewModel>(viewModel);
            }
        }
    }
}
