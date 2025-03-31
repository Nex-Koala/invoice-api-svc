using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Update.v1;

public sealed class UpdateClassificationMappingHandler(
    ILogger<UpdateClassificationMappingHandler> logger,
    [FromKeyedServices("invoice:classificationMappings")] IRepository<ClassificationMapping> repository
) : IRequestHandler<UpdateClassificationMappingCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        UpdateClassificationMappingCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var classificationMapping = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = classificationMapping ?? throw new ClassificationMappingNotFoundException(request.Id);
        var updatedClassificationMapping = classificationMapping.Update(request.LhdnClassificationCode, request.ClassificationId);
        await repository.UpdateAsync(updatedClassificationMapping, cancellationToken);
        logger.LogInformation("ClassificationMapping with id : {ClassificationMappingId} updated.", classificationMapping.Id);
        return new Response<Guid>(classificationMapping.Id);
    }
}
