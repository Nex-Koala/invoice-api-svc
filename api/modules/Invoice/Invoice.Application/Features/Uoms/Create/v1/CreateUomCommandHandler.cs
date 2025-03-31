using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.Create.v1;

public sealed class CreateUomCommandHandler(
    ILogger<CreateUomCommandHandler> logger,
    [FromKeyedServices("invoice:uoms")] IRepository<Uom> repository
) : IRequestHandler<CreateUomCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        CreateUomCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var newUom = Uom.Create(request.UserId, request.Code, request.Description);
        await repository.AddAsync(newUom, cancellationToken);
        logger.LogInformation("Uom created {UomId}", newUom.Id);
        return new Response<Guid>(newUom.Id);
    }
}
