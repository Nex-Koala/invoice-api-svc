using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Uoms.Update.v1;

public sealed class UpdateUomHandler(
    ILogger<UpdateUomHandler> logger,
    [FromKeyedServices("invoice:uoms")] IRepository<Uom> repository
) : IRequestHandler<UpdateUomCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        UpdateUomCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var uom = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = uom ?? throw new UomNotFoundException(request.Id);
        var updatedUom = uom.Update(request.Code, request.Description);
        await repository.UpdateAsync(updatedUom, cancellationToken);
        logger.LogInformation("Uom with id : {UomId} updated.", uom.Id);
        return new Response<Guid>(uom.Id);
    }
}
