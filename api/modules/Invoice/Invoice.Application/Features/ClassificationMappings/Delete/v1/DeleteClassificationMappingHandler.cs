using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Delete.v1;

public sealed class DeleteClassificationMappingHandler(
    ILogger<DeleteClassificationMappingHandler> logger,
    [FromKeyedServices("invoice:classificationMappings")] IRepository<ClassificationMapping> repository
) : IRequestHandler<DeleteClassificationMappingCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(DeleteClassificationMappingCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var classificationMapping = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = classificationMapping ?? throw new ClassificationMappingNotFoundException(request.Id);
        await repository.DeleteAsync(classificationMapping, cancellationToken);
        logger.LogInformation("ClassificationMapping with id : {ClassificationMappingId} deleted", classificationMapping.Id);

        return new Response<Guid>(classificationMapping.Id, "Mapping deleted successfully.");
    }
}
