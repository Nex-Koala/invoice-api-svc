using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Classification.Queries.GetAllClassifications
{
    public class GetAllClassificationQuery : IRequest<PagedResponse<IEnumerable<GetAllClassificationViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid UserId { get; set; }
    }
    public class GetAllClassificationQueryHandler : IRequestHandler<GetAllClassificationQuery, PagedResponse<IEnumerable<GetAllClassificationViewModel>>>
    {
        private readonly IClassificationRepositoryAsync _classificationRepository;
        private readonly IMapper _mapper;
        public GetAllClassificationQueryHandler(IClassificationRepositoryAsync classificationRepository, IMapper mapper)
        {
            _classificationRepository = classificationRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllClassificationViewModel>>> Handle(GetAllClassificationQuery request, CancellationToken cancellationToken)
        {
            // Map request parameters
            var validFilter = _mapper.Map<GetAllClassificationParameter>(request);

            // Fetch paginated response from repository
            var classifications = await _classificationRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, request.UserId);

            // Map entities to view models

            var classificationViewModels = _mapper.Map<IEnumerable<GetAllClassificationViewModel>>(classifications.Data);

            // Return paged response
            return new PagedResponse<IEnumerable<GetAllClassificationViewModel>>(classificationViewModels, classifications.PageNumber, classifications.PageSize, classifications.TotalCount);
        }
    }
}
