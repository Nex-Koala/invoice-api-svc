using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Exceptions;

namespace NexKoala.WebApi.Invoice.Application.Features.Classifications.Get.v1;

public sealed class GetClassificationHandler(
    [FromKeyedServices("invoice:classifications")] IReadRepository<Classification> repository
) : IRequestHandler<GetClassificationRequest, Response<ClassificationResponse>>
{
    public async Task<Response<ClassificationResponse>> Handle(
        GetClassificationRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var classificationItem = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (classificationItem == null)
            throw new ClassificationNotFoundException(request.Id);

        var response = new ClassificationResponse(
            classificationItem.Id,
            classificationItem.UserId,
            classificationItem.Code,
            classificationItem.Description
        );

        return new Response<ClassificationResponse>(response);
    }
}
