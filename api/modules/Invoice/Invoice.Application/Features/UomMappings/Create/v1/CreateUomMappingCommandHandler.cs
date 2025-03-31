using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.UomMappings.Create.v1;

public sealed class CreateUomMappingCommandHandler(
    ILogger<CreateUomMappingCommandHandler> logger,
    [FromKeyedServices("invoice:uomMappings")] IRepository<UomMapping> repository
) : IRequestHandler<CreateUomMappingCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        CreateUomMappingCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var newUomMapping = UomMapping.Create(request.LhdnUomCode, request.UomId);
        await repository.AddAsync(newUomMapping, cancellationToken);
        logger.LogInformation("UomMapping created {UomMappingId}", newUomMapping.Id);
        return new Response<Guid>(newUomMapping.Id);
    }
}
