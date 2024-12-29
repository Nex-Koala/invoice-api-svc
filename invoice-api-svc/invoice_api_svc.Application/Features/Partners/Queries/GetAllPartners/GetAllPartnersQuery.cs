using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Partners.Queries.GetAllPartners
{
    public class GetAllPartnersQuery : IRequest<PagedResponse<IEnumerable<PartnersViewModel>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Name { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Phone { get; set; } = null;
        public string? LicenseKey { get; set; } = null;
        public bool? Status { get; set; } = null;
    }
    public class GetAllPartnersQueryHandler : IRequestHandler<GetAllPartnersQuery, PagedResponse<IEnumerable<PartnersViewModel>>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        private readonly IMapper _mapper;
        public GetAllPartnersQueryHandler(IUserRepositoryAsync userRepositoryAsync, IMapper mapper)
        {
            _userRepositoryAsync = userRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<PartnersViewModel>>> Handle(GetAllPartnersQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetAllPartnerFilter>(request);
            var users = await _userRepositoryAsync.GetPagedResponseAsync(filter);
            var viewModel = _mapper.Map<IEnumerable<PartnersViewModel>>(users.Data);
            return new PagedResponse<IEnumerable<PartnersViewModel>>(viewModel, users.PageNumber, users.PageSize, users.TotalCount);
        }
    }
}
