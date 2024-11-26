using AutoMapper;
using invoice_api_svc.Application.Features.UomMappings.Queries.GetAllUomMappings;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.UomMappings.Queries.GetUomMappingById
{
    public class GetUomMappingByIdQuery : IRequest<Response<UomMappingViewModel>>
    {
        public int Id { get; set; }
        public class GetUomMappingByIdQueryHandler : IRequestHandler<GetUomMappingByIdQuery, Response<UomMappingViewModel>>
        {
            private readonly IUomMappingRepositoryAsync _uomMappingRepository;
            private readonly IMapper _mapper;

            public GetUomMappingByIdQueryHandler(IUomMappingRepositoryAsync uomMappingRepository, IMapper mapper)
            {
                _uomMappingRepository = uomMappingRepository;
                _mapper = mapper;
            }

            public async Task<Response<UomMappingViewModel>> Handle(GetUomMappingByIdQuery request, CancellationToken cancellationToken)
            {
                var uomMapping = await _uomMappingRepository.GetByIdAsync(request.Id);

                if (uomMapping == null)
                {
                    return new Response<UomMappingViewModel>("UOM Mapping not found");
                }

                var viewModel = _mapper.Map<UomMappingViewModel>(uomMapping);
                return new Response<UomMappingViewModel>(viewModel);
            }
        }
    }
}
