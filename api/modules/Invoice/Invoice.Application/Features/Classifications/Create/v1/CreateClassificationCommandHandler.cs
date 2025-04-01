using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Create.v1;

public sealed class CreateClassificationCommandHandler(
    ILogger<CreateClassificationCommandHandler> logger,
    [FromKeyedServices("invoice:classifications")] IRepository<Classification> repository
) : IRequestHandler<CreateClassificationCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        CreateClassificationCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var newClassification = Classification.Create(request.UserId, request.Code, request.Description);
        await repository.AddAsync(newClassification, cancellationToken);
        logger.LogInformation("Classification created {ClassificationId}", newClassification.Id);
        return new Response<Guid>(newClassification.Id);
    }
}
