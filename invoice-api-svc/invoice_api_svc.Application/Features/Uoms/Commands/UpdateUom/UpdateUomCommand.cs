using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Products.Commands.UpdateUom
{
    public class UpdateUomCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public class UpdateUomCommandHandler : IRequestHandler<UpdateUomCommand, Response<int>>
        {
            private readonly IUomRepositoryAsync _uomRepository;

            public UpdateUomCommandHandler(IUomRepositoryAsync uomRepository)
            {
                _uomRepository = uomRepository;
            }

            public async Task<Response<int>> Handle(UpdateUomCommand request, CancellationToken cancellationToken)
            {
                var uom = await _uomRepository.GetByIdAsync(request.Id);
                if (uom == null)
                {
                    throw new System.Exception($"UOM with Id {request.Id} not found.");
                }

                uom.Code = request.Code;
                uom.Description = request.Description;

                await _uomRepository.UpdateAsync(uom);

                return new Response<int>(uom.Id);
            }
        }
    }
}
