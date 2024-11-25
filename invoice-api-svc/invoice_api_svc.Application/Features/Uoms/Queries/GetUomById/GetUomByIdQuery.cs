using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using invoice_api_svc.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Products.Queries.GetUomById
{
    public class GetUomByIdQuery : IRequest<Response<Uom>>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetUomByIdQuery, Response<Uom>>
        {
            private readonly IUomRepositoryAsync _uomRepository;

            public GetProductByIdQueryHandler(IUomRepositoryAsync uomRepository)
            {
                _uomRepository = uomRepository;
            }

            public async Task<Response<Uom>> Handle(GetUomByIdQuery request, CancellationToken cancellationToken)
            {
                var uom = await _uomRepository.GetByIdAsync(request.Id);
                if (uom == null)
                {
                    throw new System.Exception($"UOM with Id {request.Id} not found.");
                }

                return new Response<Uom>(uom);
            }
        }
    }
}
