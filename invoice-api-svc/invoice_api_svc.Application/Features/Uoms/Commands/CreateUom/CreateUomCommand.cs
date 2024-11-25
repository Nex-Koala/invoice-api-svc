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
    public partial class CreateUomCommand : IRequest<Response<int>>
    {
        public Guid UserId { get; set; } // Reference to the Seller
        public string Code { get; set; } = default!; // Unique Code for UOM
        public string Description { get; set; } = default!; // Description of the UOM
    }

    public class CreateUomCommandHandler : IRequestHandler<CreateUomCommand, Response<int>>
    {
        private readonly IUomRepositoryAsync _uomRepository;
        private readonly IMapper _mapper;

        public CreateUomCommandHandler(IUomRepositoryAsync uomRepository, IMapper mapper)
        {
            _uomRepository = uomRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateUomCommand request, CancellationToken cancellationToken)
        {
            // Ensure the UOM Code is unique
            var isCodeUnique = await _uomRepository.IsCodeUniqueAsync(request.Code);
            if (!isCodeUnique)
            {
                throw new System.Exception("The UOM Code must be unique.");
            }

            // Map the request to the UOM entity
            var uom = _mapper.Map<Uom>(request);

            // Add the UOM to the database
            await _uomRepository.AddAsync(uom);

            // Return the created UOM's ID
            return new Response<int>(uom.Id);
        }
    }
}
