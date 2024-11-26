using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using AutoMapper;
using invoice_api_svc.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace invoice_api_svc.Application.Features.Uoms.Commands.CreateUom
{
    public class CreateUomCommand : IRequest<Response<int>>
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public class CreateUomCommandHandler : IRequestHandler<CreateUomCommand, Response<int>>
        {
            private readonly IUomRepositoryAsync _uomRepository;

            public CreateUomCommandHandler(IUomRepositoryAsync uomRepository)
            {
                _uomRepository = uomRepository;
            }

            public async Task<Response<int>> Handle(CreateUomCommand request, CancellationToken cancellationToken)
            {
                var newUom = new Uom
                {
                    Code = request.Code,
                    Description = request.Description
                };

                var createdUom = await _uomRepository.AddAsync(newUom);
                return new Response<int>(createdUom.Id);
            }
        }
    }
}
