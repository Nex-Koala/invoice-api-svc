using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using invoice_api_svc.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Classification.Queries.GetClassificationById
{
    public class GetClassificationByIdQuery : IRequest<Response<Domain.Entities.Classification>>
    {
        public int Id { get; set; }
        public class GetClassificationByIdQueryHandler : IRequestHandler<GetClassificationByIdQuery, Response<Domain.Entities.Classification>>
        {
            private readonly IClassificationRepositoryAsync _classificationRepository;

            public GetClassificationByIdQueryHandler(IClassificationRepositoryAsync classificationRepository)
            {
                _classificationRepository = classificationRepository;
            }

            public async Task<Response<Domain.Entities.Classification>> Handle(GetClassificationByIdQuery request, CancellationToken cancellationToken)
            {
                var classification = await _classificationRepository.GetByIdAsync(request.Id);
                if (classification == null)
                {
                    throw new System.Exception($"Classification with Id {request.Id} not found.");
                }

                return new Response<Domain.Entities.Classification>(classification);
            }
        }
    }
}
