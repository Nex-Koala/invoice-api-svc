using invoice_api_svc.Application.Filters;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Products.Queries.GetAllUoms
{
    public class GetAllUomsQuery : IRequest<PagedResponse<IEnumerable<GetAllUomsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllUomsQueryHandler : IRequestHandler<GetAllUomsQuery, PagedResponse<IEnumerable<GetAllUomsViewModel>>>
    {
        private readonly IUomRepositoryAsync _uomRepository;
        private readonly IMapper _mapper;
        public GetAllUomsQueryHandler(IUomRepositoryAsync uomRepository, IMapper mapper)
        {
            _uomRepository = uomRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllUomsViewModel>>> Handle(GetAllUomsQuery request, CancellationToken cancellationToken)
        {
            // Map request parameters
            var validFilter = _mapper.Map<GetAllUomsParameter>(request);

            // Fetch paginated response from repository
            var uoms = await _uomRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);

            // Map entities to view models
            var uomViewModels = _mapper.Map<IEnumerable<GetAllUomsViewModel>>(uoms);

            // Return paged response
            return new PagedResponse<IEnumerable<GetAllUomsViewModel>>(uomViewModels, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
