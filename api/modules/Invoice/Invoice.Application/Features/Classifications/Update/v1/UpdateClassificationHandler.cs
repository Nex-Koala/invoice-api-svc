using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Update.v1;

public sealed class UpdateClassificationHandler(
    ILogger<UpdateClassificationHandler> logger,
    [FromKeyedServices("invoice:classifications")] IRepository<Classification> repository
) : IRequestHandler<UpdateClassificationCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        UpdateClassificationCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var classification = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = classification ?? throw new ClassificationNotFoundException(request.Id);
        var updatedClassification = classification.Update(request.Code, request.Description);
        await repository.UpdateAsync(updatedClassification, cancellationToken);
        logger.LogInformation("Classification with id : {ClassificationId} updated.", classification.Id);
        return new Response<Guid>(classification.Id);
    }
}
