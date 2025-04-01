using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Delete.v1;

public sealed class DeleteUomMappingHandler(
    ILogger<DeleteUomMappingHandler> logger,
    [FromKeyedServices("invoice:uomMappings")] IRepository<UomMapping> repository
) : IRequestHandler<DeleteUomMappingCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(DeleteUomMappingCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var uomMapping = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = uomMapping ?? throw new UomMappingNotFoundException(request.Id);
        await repository.DeleteAsync(uomMapping, cancellationToken);
        logger.LogInformation("UomMapping with id : {UomMappingId} deleted", uomMapping.Id);

        return new Response<Guid>(uomMapping.Id, "Mapping deleted successfully.");
    }
}
