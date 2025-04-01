using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Delete.v1;

public sealed class DeleteClassificationHandler(
    ILogger<DeleteClassificationHandler> logger,
    [FromKeyedServices("invoice:classifications")] IRepository<Classification> repository
) : IRequestHandler<DeleteClassificationCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(DeleteClassificationCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var classification = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = classification ?? throw new ClassificationNotFoundException(request.Id);
        await repository.DeleteAsync(classification, cancellationToken);
        logger.LogInformation("Classification with id : {ClassificationId} deleted", classification.Id);

        return new Response<Guid>(classification.Id, "Classification deleted successfully.");
    }
}
