using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Update.v1;

public sealed class UpdateUomMappingHandler(
    ILogger<UpdateUomMappingHandler> logger,
    [FromKeyedServices("invoice:uomMappings")] IRepository<UomMapping> repository
) : IRequestHandler<UpdateUomMappingCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        UpdateUomMappingCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var uomMapping = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = uomMapping ?? throw new UomMappingNotFoundException(request.Id);
        var updatedUomMapping = uomMapping.Update(request.LhdnUomCode, request.UomId);
        await repository.UpdateAsync(updatedUomMapping, cancellationToken);
        logger.LogInformation("UomMapping with id : {UomMappingId} updated.", uomMapping.Id);
        return new Response<Guid>(uomMapping.Id);
    }
}
