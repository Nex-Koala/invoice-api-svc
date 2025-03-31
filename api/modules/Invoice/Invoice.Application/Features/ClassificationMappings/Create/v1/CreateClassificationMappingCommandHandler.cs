using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Create.v1;

public sealed class CreateClassificationMappingCommandHandler(
    ILogger<CreateClassificationMappingCommandHandler> logger,
    [FromKeyedServices("invoice:classificationMappings")] IRepository<ClassificationMapping> repository
) : IRequestHandler<CreateClassificationMappingCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        CreateClassificationMappingCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var newClassificationMapping = ClassificationMapping.Create(request.LhdnClassificationCode, request.ClassificationId);
        await repository.AddAsync(newClassificationMapping, cancellationToken);
        logger.LogInformation("ClassificationMapping created {ClassificationMappingId}", newClassificationMapping.Id);
        return new Response<Guid>(newClassificationMapping.Id);
    }
}
