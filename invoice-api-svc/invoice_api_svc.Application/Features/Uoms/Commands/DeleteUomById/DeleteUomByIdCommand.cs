using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Products.Commands.DeleteUomById
{
    public class DeleteUomByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteUomByIdCommandHandler : IRequestHandler<DeleteUomByIdCommand, Response<int>>
        {
            private readonly IUomRepositoryAsync _uomRepository;

            public DeleteUomByIdCommandHandler(IUomRepositoryAsync uomRepository)
            {
                _uomRepository = uomRepository;
            }

            public async Task<Response<int>> Handle(DeleteUomByIdCommand request, CancellationToken cancellationToken)
            {
                var uom = await _uomRepository.GetByIdAsync(request.Id);
                if (uom == null)
                {
                    throw new System.Exception($"UOM with Id {request.Id} not found.");
                }

                await _uomRepository.DeleteAsync(uom);

                return new Response<int>(request.Id);
            }
        }
    }
}
